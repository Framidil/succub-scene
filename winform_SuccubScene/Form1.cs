using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SuccubScene
{
    public partial class SuccubSceneForm : Form
    {
        public const int STOP_POINT_SIZE = 25;
        public const int START_POINT_SIZE = 25;

        List<StopPoint> stopPoints = new List<StopPoint>();
        int draggedStopPointIndex = -1;

        List<Group> groups = new List<Group>();
        int draggedStartPointIndex = -1;

        List<MyLine> lines = new List<MyLine>();

        List<Model> models = new List<Model>();

        float speedMult = 1.0f;

        int getStopPoindId()
        {
            int id = stopPoints.Count + 1;
            while (stopPoints.FindAll(x => x.number == id).Count != 0)
            {
                id++;
            }
            return id;
        }

        int getGroupId()
        {
            int id = groups.Count + 1;
            while (groups.FindAll(x => x.number == id).Count != 0)
            {
                id++;
            }
            return id;
        }
        int getModelId()
        {
            int id = models.Count + 1;
            while (models.FindAll(x => x.number == id).Count != 0)
            {
                id++;
            }
            return id;
        }

        int getLineId()
        {
            int id = lines.Count + 1;
            while (lines.FindAll(x => x.number == id).Count != 0)
            {
                id++;
            }
            return id;
        }

        Boolean isStarted = false;

        Timer timer = new Timer();
        public SuccubSceneForm()
        {
            InitializeComponent();

            //readInitPoints();

            timer.Tick += Timer_Tick;
        }

        private void readInitPoints()
        {
            var fileData = "30,30;30,350;600,30;600,350\n500,200;200,200";
            var f = fileData.Split('\n');
            foreach (var stopPoint in f[0].Split(';'))
            {
                var coords = stopPoint.Split(',');
                stopPoints.Add(new StopPoint(int.Parse(coords[0]), int.Parse(coords[1]), getStopPoindId()));
            }

            foreach (var stopPoint in f[1].Split(';'))
            {
                var coords = stopPoint.Split(',');
                groups.Add(new Group(int.Parse(coords[0]), int.Parse(coords[1]), getGroupId()));
            }

            modelsSpeedTextBox.Text = "100";
        }

        Dictionary<String, int> usedLinesString = new Dictionary<String, int>();
        protected override void OnPaint(PaintEventArgs e)
        {
            var textFont = new System.Drawing.Font("Arial", 14);
            foreach (StopPoint stopPoint in stopPoints)
            {
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(stopPoint.x, stopPoint.y, STOP_POINT_SIZE, STOP_POINT_SIZE));
                var deltaX = 4;
                if (stopPoint.number >= 10)
                {
                    deltaX = -1;
                }
                var s = stopPoint.number.ToString();
                if (stopPoint.delayInMs != 0)
                {
                    s += "(" + stopPoint.delayInMs / 1000 + "с.)";
                }
                e.Graphics.DrawString(s, textFont, Pens.Black.Brush, stopPoint.x + deltaX, stopPoint.y);
            }

            foreach (Group group in groups)
            {
                var deltaX = 4;
                if (group.number >= 10)
                {
                    deltaX = -1;
                }
                e.Graphics.DrawRectangle(Pens.Red, new Rectangle(group.x, group.y, START_POINT_SIZE, START_POINT_SIZE));
                e.Graphics.DrawString(group.number.ToString(), textFont, Pens.Black.Brush, group.x + deltaX, group.y);
            }


            Random random = new Random();
            usedLinesString.Clear();
            foreach (MyLine line in lines)
            {
                int diap = 15;
                e.Graphics.DrawLine(Pens.Red, line.x1, line.y1, line.x2, line.y2);
                if (draggedStartPointIndex == -1 && draggedStopPointIndex == -1)
                {
                    int x = (line.x1 + line.x2) / 2;
                    int y = (line.y1 + line.y2) / 2;
                    while (usedLinesString.ContainsKey(x + "|" + y))
                    {
                        y += 15;
                    }
                    usedLinesString[x + "|" + y] = 1;
                    e.Graphics.DrawString(line.number.ToString() + "(" + (float)line.lengthInSm / 100 + "м.)",
                        textFont, Pens.Black.Brush, x, y);
                }
            }

            if (!isStarted) return;
            foreach (Model model in models)
            {
                if (model.x == -1 || model.y == -1)
                {
                    continue;
                }

                var x = model.x;
                var y = model.y;
                float radius = 12.5f;
                var deltaX = 4;
                if (model.number >= 10)
                {
                    deltaX = -1;
                }
                e.Graphics.DrawEllipse(Pens.Red, x - radius, y - radius, 2 * radius, 2 * radius);
                e.Graphics.FillEllipse(new SolidBrush(Color.IndianRed), x - radius, y - radius, 2 * radius, 2 * radius);

                e.Graphics.DrawString(model.number.ToString(), textFont, Pens.Black.Brush, x - radius + deltaX, y - radius);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left || isStarted) return;

            int i = 0;
            foreach (StopPoint point in stopPoints)
            {
                if (intersect(point.x, point.y, point.x + STOP_POINT_SIZE, point.y + START_POINT_SIZE, e.X, e.Y))
                {
                    draggedStopPointIndex = i;
                    showStopPanel(i);
                    return;
                }
                i++;
            }

            i = 0;
            foreach (Group group in groups)
            {
                if (intersect(group.x, group.y, group.x + STOP_POINT_SIZE, group.y + START_POINT_SIZE, e.X, e.Y))
                {
                    draggedStartPointIndex = i;
                    showGroupPanel(i);
                    return;
                }
                i++;
            }
        }
        int selectedStopPointIndex = -1;
        int selectedGroupIndex = -1;
        private void showStopPanel(int stopPointIndex)
        {
            selectedStopPointIndex = stopPointIndex;
            stopPointPanel.Visible = true;
            linePanel.Visible = false;
            groupPanel.Visible = false;

            stopPointNameLabel.Text = "Точка номер " + stopPoints[selectedStopPointIndex].number;
            stopPointPauseTextBox.Text = "" + (float)stopPoints[selectedStopPointIndex].delayInMs / 1000;
        }

        private void DeleteStopPointButton_Click(object sender, EventArgs e)
        {
            foreach (var g in groups)
            {
                for (int i = 0; i < g.path.Count - 1; i++)
                {
                    if (g.path[i] == stopPoints[selectedStopPointIndex].number)
                    {
                        MessageBox.Show("Невозможно удалить, так как точка входит в определенный путь.");
                        return;
                    }
                }
            }

            stopPoints.RemoveAt(selectedStopPointIndex);
            stopPointPanel.Visible = false;
            selectedStopPointIndex = -1;
            this.Invalidate(true);
        }

        private void showGroupPanel(int groupIndex)
        {
            selectedGroupIndex = groupIndex;
            stopPointPanel.Visible = false;
            linePanel.Visible = false;
            groupPanel.Visible = true;

            var group = groups[selectedGroupIndex];
            groupNameLabel.Text = "Выход номер " + group.number;
            numberOfModelsTextBox.Text = group.modelsNumber.ToString();
            groupDelayTextBox.Text = (group.delayFromStartInMs / 1000).ToString();
            groupPathTextBox.Text = String.Join(", ", group.path);
            groupPeriodTextBox.Text = (group.periodInMs / 1000).ToString();
        }

        private bool intersect(int x1, int y1, int x2, int y2, int x, int y)
        {
            return x1 <= x && x <= x2 && y1 <= y && y <= y2;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (draggedStopPointIndex != -1)
            {
                stopPoints[draggedStopPointIndex].x = e.X - STOP_POINT_SIZE / 2;
                stopPoints[draggedStopPointIndex].y = e.Y - STOP_POINT_SIZE / 2;

                recreateLines();
            }

            if (draggedStartPointIndex != -1)
            {
                groups[draggedStartPointIndex].x = e.X - START_POINT_SIZE / 2;
                groups[draggedStartPointIndex].y = e.Y - START_POINT_SIZE / 2;
                recreateLines();
            }

            if (draggedStartPointIndex != -1 || draggedStopPointIndex != -1)
            {
                this.Invalidate(true);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            draggedStartPointIndex = -1;
            draggedStopPointIndex = -1;
            this.Invalidate(true);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            isStarted = !isStarted;
            startButton.Text = isStarted ? "Остановить" : "Начать";

            if (isStarted)
            {
                startJob();
            }
            else
            {
                stopJob();
            }
        }

        int curIndex = 0;
        private void stopJob()
        {
            timer.Stop();
            moveTasks.Clear();
            models.Clear();
            curIndex = 0;
            this.Invalidate(true);
        }

        long startMs = -1;
        private void startJob()
        {
            if (modelsSpeedTextBox.Text.Length == 0 || int.Parse(modelsSpeedTextBox.Text) == 0)
            {
                MessageBox.Show("Задайте скорость моделей.");
                return;
            }
            if (groups.Count == 0)
            {
                MessageBox.Show("Нет ни одного выхода.");
                return;
            }
            lastSec = -1;
            startMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            calcAllPath();

            timerLabel.Text = "Время: 00:00";
            timer.Interval = 1000 / 15;
            timer.Start();
        }

        List<MoveTask> moveTasks = new List<MoveTask>();
        private void calcAllPath()
        {
            long FPS = 60;

            foreach (var g in groups)
            {
                var modelsCount = g.modelsNumber;
                long startTimeMs = g.delayFromStartInMs;

                for (int i = 0; i < modelsCount; i++)
                {
                    var newModel = new Model(-1, -1, getModelId());
                    long startPathTimeInMs = startTimeMs;
                    for (int j = -1; j < g.path.Count - 1; j++)
                    {
                        int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
                        long d = 0;
                        MyLine line = null;
                        if (j == -1)
                        {
                            var p1 = g;
                            var p2 = stopPoints.Find(p => p.number == g.path[0]);

                            x1 = p1.x; y1 = p1.y; x2 = p2.x; y2 = p2.y;
                            line = findLine(-p1.number - 1, p2.number, g.number);
                            d = p2.delayInMs;
                        }
                        else if (j == g.path.Count - 2)
                        {
                            var p1 = stopPoints.Find(p => p.number == g.path[g.path.Count - 2]);
                            var p2 = groups.Find(g1 => g1.number == g.path[g.path.Count - 1]);

                            x1 = p1.x; y1 = p1.y; x2 = p2.x; y2 = p2.y;
                            line = findLine(p1.number, -p2.number - 1, g.number);
                        }
                        else
                        {
                            var p1 = stopPoints.Find(p => p.number == g.path[j]);
                            var p2 = stopPoints.Find(p => p.number == g.path[j + 1]);

                            x1 = p1.x; y1 = p1.y; x2 = p2.x; y2 = p2.y;

                            line = findLine(p1.number, p2.number, g.number);
                            d = p2.delayInMs;
                        }

                        long endTimeInMs = startPathTimeInMs + (long)(line.lengthInSm /
                            (float)int.Parse(modelsSpeedTextBox.Text)) * 1000;
                        //Console.WriteLine("StartTime: " + startPathTimeInMs);
                        //Console.WriteLine("EndTime: " + endTimeInMs);
                        for (long k = 0; k <= FPS; k++)
                        {
                            var newX = x1 + (k * (x2 - x1)) / FPS + STOP_POINT_SIZE / 2;
                            var newY = y1 + (k * (y2 - y1)) / FPS + STOP_POINT_SIZE / 2;
                            var newTime = startPathTimeInMs + (k * (endTimeInMs - startPathTimeInMs)) / FPS;
                            var newTask = new MoveTask((int)newX, (int)newY, (long)newTime, newModel.number);
                            //Console.WriteLine("x: " + (int)newX + ", y: " + (int)newY + ", time: " + (long)newTime);
                            moveTasks.Add(newTask);
                        }

                        startPathTimeInMs = endTimeInMs + d;
                        //Console.WriteLine("END PATH.\n\n");
                    }

                    var removeTask = new MoveTask(-1, -1, startPathTimeInMs + 1, newModel.number);
                    moveTasks.Add(removeTask);
                    models.Add(newModel);

                    startTimeMs += g.periodInMs;
                }

                moveTasks.Sort((a, b) => a.timeMs < b.timeMs ? -1 : 1);
            }
        }

        private MyLine findLine(int number1, int number2, int pNumber)
        {
            //Console.WriteLine("lines: ");
            foreach (var line in lines)
            {
                // Console.Write("[" + line.par1Number + ", " + line.par2Number + "], ");
            }
            //Console.WriteLine("number1: " + number1 + ", number2: " + number2 + ", parentGroupNumber: " + pNumber);
            var ln = lines.Find(l => l.parentGroupNumber == pNumber && l.par1Number == number1 && l.par2Number == number2);
            //Console.WriteLine(ln);

            return ln;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            long currentTimeInMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            updateTimer(currentTimeInMs);
            moveObjects(currentTimeInMs);
        }


        private void moveObjects(long currentTimeInMs)
        {
            var top = 1L;
            var bot = 1L;
            if (speedMult < 1.1)
            {
                bot = 1; top = 1;
            } else if (speedMult < 1.9)
            {
                top = 2; bot = 3;
            } else if (speedMult < 2.9) {
                top = 1; bot = 2;
            } else
            {
                top = 1; bot = 3;
            }
            
            while (curIndex < moveTasks.Count && moveTasks[curIndex].timeMs * top + startMs * bot <= currentTimeInMs * bot)
            {
                var model = models.Find(m => m.number == moveTasks[curIndex].modelNumber);
                model.x = moveTasks[curIndex].newX;
                model.y = moveTasks[curIndex].newY;
                curIndex++;
            }
            if (curIndex == moveTasks.Count)
            {
                startButton_Click(null, null);
                return;
            }
            this.Invalidate(true);
        }

        int lastSec = -1;
        private void updateTimer(long currentTimeInMs)
        {
            int totalSec = (int)((currentTimeInMs - startMs) * speedMult) / 1000;
/*            Console.WriteLine("currentTimeInMs: " + currentTimeInMs);
            Console.WriteLine("totalSec: " + totalSec);*/
            int sec = totalSec % 60;

            if (sec == lastSec) return;
            lastSec = sec;

            var secM = sec.ToString();
            if (secM.Length == 1) secM = "0" + secM;


            int min = totalSec / 60;
            var minM = min.ToString();
            if (minM.Length == 1) minM = "0" + minM;

            timerLabel.Text = "Время: " + minM + ":" + secM;
        }

        private void addStopPointButton_Click(object sender, EventArgs e)
        {
            stopPoints.Add(new StopPoint(40, 40, getStopPoindId()));

            this.Invalidate(true);
        }

        private void addStartPointButton_Click(object sender, EventArgs e)
        {
            groups.Add(new Group(40, 40, getGroupId()));
            this.Invalidate(true);
        }

        private void stopPointPauseTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void stopPointPauseTextBox_TextChanged(object sender, EventArgs e)
        {
            if (stopPointPauseTextBox.Text.Length != 0)
                stopPoints[selectedStopPointIndex].delayInMs = int.Parse(stopPointPauseTextBox.Text) * 1000;
            this.Invalidate(true);
        }

        private void numberOfModelsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void groupDelayTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void groupPeriodTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void deleteGroupButton_Click(object sender, EventArgs e)
        {
            groups.RemoveAt(selectedGroupIndex);
            groupPanel.Visible = false;
            selectedGroupIndex = -1;

            lines.Clear();
            foreach (var group in groups)
            {
                createLines(group.path, group.number);
            }

            this.Invalidate(true);
        }

        private void saveGroupButton_Click(object sender, EventArgs e)
        {
            int modelsNumber = -1;
            int delayFromStart = -1;
            int period = -1;
            List<int> path = null;

            int.TryParse(numberOfModelsTextBox.Text, out modelsNumber);
            int.TryParse(groupDelayTextBox.Text, out delayFromStart);
            int.TryParse(groupPeriodTextBox.Text, out period);

            var rawPath = groupPathTextBox.Text;
            var rawPathWithoutSpaces = "";
            foreach (var ch in rawPath)
            {
                if (ch != ' ') rawPathWithoutSpaces += ch;
            }

            var nms = rawPathWithoutSpaces.Split(',');
            foreach (var n in nms)
            {
                int t = -1;
                if (!int.TryParse(n, out t) || (n != nms[nms.Length - 1] && stopPoints.FindAll(x => x.number == t).Count == 0)
                    || (n == nms[nms.Length - 1] && groups.FindAll(x => x.number == t).Count == 0))
                {
                    path = null;
                    break;
                }
                else
                {
                    if (path == null)
                    {
                        path = new List<int>();
                    }
                    path.Add(t);
                }
            }

            if (rawPathWithoutSpaces.Length == 0)
            {
                path = new List<int>();
            }

            if (modelsNumber == -1 || delayFromStart == -1 || period == -1 || path == null || (path.Count != 0 && path.Count < 3))
            {
                MessageBox.Show("Неверно заполненное поле.");
            }
            else
            {
                var group = groups[selectedGroupIndex];
                group.modelsNumber = modelsNumber;
                group.path = path;


                var llns = lines.FindAll(x => x.parentGroupNumber == group.number);
                lines.RemoveAll(x => x.parentGroupNumber == group.number);

                createLines(path, group.number);
                foreach (var line in llns)
                {
                    var a = lines.FindAll(l => l.parentGroupNumber == group.number
                        && l.par1Number == line.par1Number
                        && l.par2Number == line.par2Number);
                    if (a.Count != 0)
                    {
                        a[0].lengthInSm = line.lengthInSm;
                    }
                }


                this.Invalidate(true);
                group.delayFromStartInMs = delayFromStart * 1000;
                group.periodInMs = period * 1000;
            }
        }

        private void createLines(List<int> path, int grNumber)
        {
            if (path.Count == 0) return;
            var delta = STOP_POINT_SIZE / 2;

            var parentGroup = groups.Find((g) => g.number == grNumber);

            var stp1 = stopPoints.Find((p) => p.number == path[0]);
            var fLine = new MyLine(parentGroup.x + delta, parentGroup.y + delta,
                stp1.x + delta, stp1.y + delta, getLineId());
            fLine.par1Number = -parentGroup.number - 1;
            fLine.par2Number = stp1.number;
            fLine.parentGroupNumber = parentGroup.number;


            /*if (lines.FindAll((l) => l.x1 == fLine.x1 && l.x2 == fLine.x2 && l.y1 == fLine.y1 && l.y2 == fLine.y2).Count == 0)
            {
                lines.Add(fLine);
            }*/
            lines.Add(fLine);
            for (int i = 0; i < path.Count - 2; i++)
            {
                var stopPoint1 = stopPoints.Find((p) => p.number == path[i]);
                var stopPoint2 = stopPoints.Find((p) => p.number == path[i + 1]);

                var line = new MyLine(stopPoint1.x + delta, stopPoint1.y + delta,
                    stopPoint2.x + delta, stopPoint2.y + delta, getLineId());
                line.par1Number = stopPoint1.number;
                line.par2Number = stopPoint2.number;
                line.parentGroupNumber = parentGroup.number;
                /*if (lines.FindAll((l) => l.x1 == line.x1 && l.x2 == line.x2 && l.y1 == line.y1 && l.y2 == line.y2).Count == 0)
                {
                    lines.Add(line);
                }*/
                lines.Add(line);
            }

            var endGr = groups.Find((g) => g.number == path[path.Count - 1]);
            var endP = stopPoints.Find(p => p.number == path[path.Count - 2]);
            var endLine = new MyLine(endP.x + delta, endP.y + delta,
                endGr.x + delta, endGr.y + delta, getLineId());
            endLine.par1Number = endP.number;
            endLine.par2Number = -endGr.number - 1;
            endLine.parentGroupNumber = parentGroup.number;
            lines.Add(endLine);
        }

        private void recreateLines()
        {
            foreach (var line in lines)
            {
                line.refresh(ref stopPoints, ref groups);
            }
        }

        private void lineEditTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        int selectedLineNumber = -1;
        private void lineEditTextBox_TextChanged(object sender, EventArgs e)
        {
            int number = -1;
            if (!int.TryParse(lineEditTextBox.Text, out number))
            {
                linePanel.Visible = false;
                return;
            }

            var liness = lines.FindAll(x => x.number == number);
            if (liness.Count == 0)
            {
                linePanel.Visible = false;
                return;
            }
            selectedLineNumber = number;
            lineSelected();
            this.Invalidate(true);
        }

        private void lineSelected()
        {
            stopPointPanel.Visible = false;
            groupPanel.Visible = false;
            linePanel.Visible = true;

            lineEditNameLabel.Text = "Линия номер: " + selectedLineNumber;
            lineLengthEditTextBox.Text = lines.Find(x => x.number == selectedLineNumber).lengthInSm.ToString();
        }

        private void lineLengthEditTextBox_TextChanged(object sender, EventArgs e)
        {
            lines.Find(x => x.number == selectedLineNumber).lengthInSm = int.Parse(lineLengthEditTextBox.Text);
            this.Invalidate(true);
        }

        private void x1SpeedButton_Click(object sender, EventArgs e)
        {
            if (isStarted) return;
            speedMult = 1f;
            x1SpeedButton.BackColor = SystemColors.ActiveCaption;
            x1_5SpeedButton.BackColor = SystemColors.Control;
            x2SpeedButton.BackColor = SystemColors.Control;
            x3SpeedButton.BackColor = SystemColors.Control;

        }

        private void x1_5SpeedButton_Click(object sender, EventArgs e)
        {
            if (isStarted) return;
            speedMult = 1.5f;
            x1SpeedButton.BackColor = SystemColors.Control;
            x1_5SpeedButton.BackColor = SystemColors.ActiveCaption;
            x2SpeedButton.BackColor = SystemColors.Control;
            x3SpeedButton.BackColor = SystemColors.Control;
        }

        private void x2SpeedButton_Click(object sender, EventArgs e)
        {
            if (isStarted) return;
            speedMult = 2f;
            x1SpeedButton.BackColor = SystemColors.Control;
            x1_5SpeedButton.BackColor = SystemColors.Control;
            x2SpeedButton.BackColor = SystemColors.ActiveCaption;
            x3SpeedButton.BackColor = SystemColors.Control;
        }

        private void x3SpeedButton_Click(object sender, EventArgs e)
        {
            if (isStarted) return;
            speedMult = 3f;
            x1SpeedButton.BackColor = SystemColors.Control;
            x1_5SpeedButton.BackColor = SystemColors.Control;
            x2SpeedButton.BackColor = SystemColors.Control;
            x3SpeedButton.BackColor = SystemColors.ActiveCaption;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string res = "";
            var sp1 = new List<String>();
            foreach (var sp in stopPoints)
            {
                sp1.Add(sp.delayInMs + "," + sp.x + "," + sp.y + "," + sp.number);
            }
            var line1 = String.Join("|", sp1);

            var g1 = new List<String>();
            foreach (var g in groups)
            {
                var s = g.delayFromStartInMs + "," + 
                    g.modelsNumber + "," +
                    g.periodInMs + "," + 
                    g.x + "," + 
                    g.y + "," +
                    g.number;
                if (g.path.Count > 0)
                {
                    s += "," + String.Join(",", g.path);
                }
                g1.Add(s);
            }
            var line2 = String.Join("|", g1);

            var l1 = new List<String>();
            foreach (var l in lines)
            {
                var s = l.x1 + "," +
                    l.x2 + "," +
                    l.y1 + "," +
                    l.y2 + "," +
                    l.number + "," +
                    l.lengthInSm + "," +
                    l.par1Number + "," +
                    l.par2Number + "," +
                    l.parentGroupNumber;

                l1.Add(s);
            }
            var line3 = String.Join("|", l1);
            var line4 = modelsSpeedTextBox.Text.Length == 0 ? 0 : int.Parse(modelsSpeedTextBox.Text);
            res = line1 + "\n" + line2 + "\n" + line3 + "\n" + line4;
            Console.WriteLine(res);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Succub data (*.succubdata)|*.succubdata";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName.ToString());
                file.WriteLine(res);
                file.Close();
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл";
            openFileDialog.Filter = "Succub data (*.succubdata)|*.succubdata";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                var filePath = openFileDialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                {
                    var fileContent = reader.ReadToEnd();
                    Console.WriteLine(fileContent);
                    readAll(fileContent);
                }
            }
        }

        private void readAll(string fileContent)
        {
            stopPoints.Clear();
            groups.Clear();
            lines.Clear();

            var lines_ = fileContent.Split('\n');
            var line1 = lines_[0];
            if (line1.Trim().Length != 0)
            {
                var ar = line1.Split('|');
                foreach (var sps in ar)
                {
                    var f = sps.Split(',');
                    var delayInMs = int.Parse(f[0]);
                    var x = int.Parse(f[1]);
                    var y = int.Parse(f[2]);
                    var number = int.Parse(f[3]);

                    var sp = new StopPoint(x, y, number);
                    sp.delayInMs = delayInMs;
                    stopPoints.Add(sp);
                }
            }


            var line2 = lines_[1];
            if (line2.Trim().Length != 0)
            {
                var ar = line2.Split('|');
                foreach (var sps in ar)
                {
                    var f = sps.Split(',');
                    var delayFromStartInMs = int.Parse(f[0]);
                    var modelsNumber = int.Parse(f[1]);
                    var periodInMs = int.Parse(f[2]);
                    var x = int.Parse(f[3]);
                    var y = int.Parse(f[4]);
                    var number = int.Parse(f[5]);
                    var path = new List<int>();
                    for (var i = 6; i < f.Length; i++)
                    {
                        path.Add(int.Parse(f[i]));
                    }

                    var gr = new Group(x, y, number);
                    gr.delayFromStartInMs = delayFromStartInMs;
                    gr.modelsNumber = modelsNumber;
                    gr.periodInMs = periodInMs;
                    gr.path = path;
                    groups.Add(gr);
                }
            }

            var line3 = lines_[2];
            if (line3.Trim().Length != 0)
            {
                var ar = line3.Split('|');
                foreach (var sps in ar)
                {
                    var f = sps.Split(',');

                    var x1 = int.Parse(f[0]);
                    var x2 = int.Parse(f[1]);
                    var y1 = int.Parse(f[2]);
                    var y2 = int.Parse(f[3]);
                    var number = int.Parse(f[4]);
                    var lengthInSm = int.Parse(f[5]);
                    var par1Number = int.Parse(f[6]);
                    var par2Number = int.Parse(f[7]);
                    var parentGroupNumber = int.Parse(f[8]);

                    var ln = new MyLine(x1, y1, x2, y2, number);
                    ln.lengthInSm = lengthInSm;
                    ln.par1Number = par1Number;
                    ln.par2Number = par2Number;
                    ln.parentGroupNumber = parentGroupNumber;

                    lines.Add(ln);
                }
            }
            var line4 = lines_[3];
            modelsSpeedTextBox.Text = line4;
        }

        class Model
        {
            public int x = -1;
            public int y = -1;
            public int number;
            public Model(int x, int y, int number)
            {
                this.x = x;
                this.y = y;
                this.number = number;
            }
        }

        class StopPoint
        {
            public int delayInMs = 0;

            public int x;
            public int y;
            public int number;

            public StopPoint(int x, int y, int number)
            {
                this.x = x; this.y = y; this.number = number;
            }
        }

        class Group
        {
            public int delayFromStartInMs = 0;
            public int modelsNumber = 1;
            public int periodInMs = 0;
            public List<int> path = new List<int>();

            public int x;
            public int y;
            public int number;

            public Group(int x, int y, int number)
            {
                this.x = x; this.y = y; this.number = number;
            }
        }

        class MyLine
        {
            public int x1, x2, y1, y2;
            public int number;

            public int lengthInSm = 300;

            public int par1Number = 0;
            public int par2Number = 0;

            public int parentGroupNumber = -1;

            public MyLine(int x1, int y1, int x2, int y2, int number)
            {
                this.x1 = x1; this.y1 = y1;
                this.x2 = x2; this.y2 = y2;
                this.number = number;
            }

            public void refresh(ref List<StopPoint> stopPoints, ref List<Group> groups)
            {
                var pointDelta = 25 / 2;
                var groupDelta = 25 / 2;
                if (par1Number < 0)
                {
                    var p1 = groups.Find((g) => g.number == (-par1Number - 1));
                    x1 = p1.x + groupDelta; y1 = p1.y + groupDelta;
                }
                else
                {
                    var p1 = stopPoints.Find((s) => s.number == par1Number);
                    x1 = p1.x + pointDelta; y1 = p1.y + pointDelta;
                }

                if (par2Number < 0)
                {
                    var p2 = groups.Find((g) => g.number == (-par2Number - 1));
                    x2 = p2.x + pointDelta; y2 = p2.y + pointDelta;
                }
                else
                {
                    var p2 = stopPoints.Find((s) => s.number == par2Number);
                    x2 = p2.x + pointDelta; y2 = p2.y + pointDelta;
                }
            }

        }

        class MoveTask
        {
            public int modelNumber;
            public int newX;
            public int newY;
            public long timeMs;

            public MoveTask(int newX, int newY, long timeMs, int modelNumber)
            {
                this.newX = newX;
                this.newY = newY;
                this.modelNumber = modelNumber;
                this.timeMs = timeMs;
            }
        }

        private void modelsSpeedTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void timerLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
