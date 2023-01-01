using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;

namespace SuccubScene
{

    public partial class MainWindow : Window
    {
        public static int STOP_POINT_SIZE = 25;
        public static int START_POINT_SIZE = 25;

        List<StopPoint> stopPoints = new List<StopPoint>();
        StopPoint draggedStopPoint = null;
        StopPoint selectedStopPoint = null;
        
        List<Group> groups = new List<Group>();
        Group draggedGroup = null;
        Group selectedGroup = null;
        MyLine selectedLine = null;

        bool isStarted = false;


        List<MyLine> lines = new List<MyLine>();

        List<Model> models = new List<Model>();

        float speedMult = 1.0f;

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();


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

        public MainWindow()
        {
            InitializeComponent();
            linePanel.Visibility = Visibility.Collapsed;
            stopPointPanel.Visibility = Visibility.Collapsed;
            groupPanel.Visibility = Visibility.Collapsed;
            x1SpeedButton.Background = SystemColors.ActiveCaptionBrush;

            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);

            //loadTest();
        }

        private void loadTest()
        {
            addStartPointButton_Click(null, null);
            addStopPointButton_Click(null, null);
            addStopPointButton_Click(null, null);
            addStopPointButton_Click(null, null);

            groups[0].path = new List<int>();
            groups[0].path.Add(1);
            groups[0].path.Add(2);
            groups[0].path.Add(3);
            groups[0].path.Add(1);

            groups[0].x = 50;
            groups[0].y = 350;
            groups[0].redraw();


            stopPoints[0].x = 50;
            stopPoints[0].y = 50;

            stopPoints[1].x = 350;
            stopPoints[1].y = 50;

            stopPoints[2].x = 350;
            stopPoints[2].y = 350;



            foreach (var stopPoint in stopPoints) stopPoint.redraw();
            recreateLines();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            Trace.WriteLine(e.Text);
            e.Handled = regex.IsMatch(e.Text);
        }

        private void addStopPointButton_Click(object sender, RoutedEventArgs e)
        {
            var newPoint = new StopPoint(50, 50, getStopPoindId(), myCanvas);
            stopPoints.Add(newPoint);
        }

        private void addStartPointButton_Click(object sender, RoutedEventArgs e)
        {
            var newGroup = new Group(50, 50, getGroupId(), myCanvas);
            groups.Add(newGroup);
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var p = e.GetPosition(this);
            if (isStarted) return;
            foreach (StopPoint point in stopPoints)
            {
                if (intersect(point.x, point.y, point.x + STOP_POINT_SIZE, point.y + START_POINT_SIZE, p.X, p.Y))
                {
                    draggedStopPoint = point;
                    showStopPanel(point);
                    return;
                }
            }

            foreach (Group group in groups)
            {
                if (intersect(group.x, group.y, group.x + STOP_POINT_SIZE, group.y + START_POINT_SIZE, p.X, p.Y))
                {
                    draggedGroup = group;
                    showGroupPanel(group);
                    return;
                }
            }

        }

        private void showGroupPanel(Group group)
        {
            linePanel.Visibility = Visibility.Collapsed;
            stopPointPanel.Visibility = Visibility.Collapsed;
            groupPanel.Visibility = Visibility.Visible;

            selectedGroup = group;

            groupNameLabel.Text = "Выход номер " + group.number;
            numberOfModelsTextBox.Text = group.modelsNumber.ToString();
            groupDelayTextBox.Text = (group.delayFromStartInMs / 1000).ToString();
            groupPathTextBox.Text = String.Join(", ", group.path);
            groupPeriodTextBox.Text = (group.periodInMs / 1000).ToString();

        }

        private void showStopPanel(StopPoint stopPoint)
        {
            selectedStopPoint = stopPoint;

            linePanel.Visibility = Visibility.Collapsed;
            stopPointPanel.Visibility = Visibility.Visible;
            groupPanel.Visibility = Visibility.Collapsed;

            stopPointNameLabel.Text = "Точка номер " + stopPoint.number;
            stopPointPauseTextBox.Text = "" + (float)stopPoint.delayInMs / 1000;

        }

        private bool intersect(int x1, int y1, int x2, int y2, double x, double y)
        {
            return x1 <= x && x <= x2 && y1 <= y && y <= y2;
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            draggedGroup = null;
            draggedStopPoint = null;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(this);
            if (draggedStopPoint != null)
            {
                draggedStopPoint.x = (int)p.X - STOP_POINT_SIZE / 2;
                draggedStopPoint.y = (int)p.Y - STOP_POINT_SIZE / 2;
                draggedStopPoint.redraw();
                refreshLines();
            }

            if (draggedGroup != null)
            {
                draggedGroup.x = (int)p.X - START_POINT_SIZE / 2;
                draggedGroup.y = (int)p.Y - START_POINT_SIZE / 2;
                draggedGroup.redraw();
               refreshLines();
            }
        }

        private void refreshLines()
        {
            foreach (var line in lines) line.refresh();
        }

        private void stopPointPauseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var t = 0;
            if (stopPointPauseTextBox.Text.Length != 0)
            {
                t = int.Parse(stopPointPauseTextBox.Text);
            }
            selectedStopPoint.delayInMs = t * 1000;
            selectedStopPoint.refreshDelay();
        }


        private void lineEditTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = -1;
            if (!int.TryParse(lineEditTextBox.Text, out number))
            {
                linePanel.Visibility = Visibility.Collapsed;
                return;
            }

            var liness = lines.FindAll(x => x.number == number);
            if (liness.Count == 0)
            {
                linePanel.Visibility = Visibility.Collapsed;
                return;
            }
            selectedLine = liness[0];
            stopPointPanel.Visibility = Visibility.Collapsed;
            groupPanel.Visibility = Visibility.Collapsed;
            linePanel.Visibility = Visibility.Visible;

            lineEditNameLabel.Text = "Линия номер: " + selectedLine.number;
            lineLengthEditTextBox.Text = selectedLine.lengthInSm.ToString();
        }

        private void lineLengthEditTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            selectedLine.lengthInSm = int.Parse(lineLengthEditTextBox.Text);
            selectedLine.refreshLength();
        }

        private void x1SpeedButton_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted) return;
            speedMult = 1f;
            x1SpeedButton.Background = SystemColors.ActiveCaptionBrush;
            x1_5SpeedButton.Background = SystemColors.ControlBrush;
            x2SpeedButton.Background = SystemColors.ControlBrush;
            x3SpeedButton.Background = SystemColors.ControlBrush;
        }

        private void x1_5SpeedButton_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted) return;
            speedMult = 1.5f;
            x1SpeedButton.Background = SystemColors.ControlBrush;
            x1_5SpeedButton.Background = SystemColors.ActiveCaptionBrush;
            x2SpeedButton.Background = SystemColors.ControlBrush;
            x3SpeedButton.Background = SystemColors.ControlBrush;
        }

        private void x2SpeedButton_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted) return;
            speedMult = 2f;
            x1SpeedButton.Background = SystemColors.ControlBrush;
            x1_5SpeedButton.Background = SystemColors.ControlBrush;
            x2SpeedButton.Background = SystemColors.ActiveCaptionBrush;
            x3SpeedButton.Background = SystemColors.ControlBrush;
        }

        private void x3SpeedButton_Click(object sender, RoutedEventArgs e)
        {
            if (isStarted) return;
            speedMult = 3f;
            x1SpeedButton.Background = SystemColors.ControlBrush;
            x1_5SpeedButton.Background = SystemColors.ControlBrush;
            x2SpeedButton.Background = SystemColors.ControlBrush;
            x3SpeedButton.Background = SystemColors.ActiveCaptionBrush;
        }

        private void saveGroupButton_Click(object sender, RoutedEventArgs e)
        {
            int modelsNumber = -1;
            int delayFromStart = -1;
            int period = -1;
            List<int> path = null;

            int.TryParse(numberOfModelsTextBox.Text, out modelsNumber);
            int.TryParse(groupDelayTextBox.Text, out delayFromStart);
            int.TryParse(groupPeriodTextBox.Text, out period);
            Trace.WriteLine("period: " + period);

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
                if (!int.TryParse(n, out t) || (n != nms[nms.Length - 1] && !stopPoints.Exists(x => x.number == t))
                    || (n == nms[nms.Length - 1] && !groups.Exists(x => x.number == t)))
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

            if (numberOfModelsTextBox.Text.Length == 0 ||
                groupDelayTextBox.Text.Length == 0 ||
                groupPeriodTextBox.Text.Length == 0)
            {
                MessageBox.Show("Заполните все поля группы.", "Невозможно создать группу");
            }
            else if (modelsNumber == -1 || delayFromStart == -1 || period == -1 || path == null || (path.Count != 0 && path.Count < 3))
            {
                MessageBox.Show("Корректно заполните все поля.", "Невозможно создать группу");
            }
            else
            {
                var group = selectedGroup;
                group.modelsNumber = modelsNumber;
                group.path = path;
                group.delayFromStartInMs = delayFromStart * 1000;
                group.periodInMs = period * 1000;

                recreateLines();

            }
        }

        private void recreateLines()
        {
            List<List<Object>> oldLines = new List<List<Object>>();
            foreach (var line in lines)
            {
                var l = new List<Object>();
                l.Add(line.startPoint);
                l.Add(line.endPoint);
                l.Add(line.startGr);
                l.Add(line.endGr);
                l.Add(line.lengthInSm);
                oldLines.Add(l);

                if (line.line != null)
                {
                    myCanvas.Children.Remove(line.line);
                    myCanvas.Children.Remove(line.textBlock);
                }
            }

            lines.Clear();
            for (int j = 0; j < groups.Count; j++)
            {
                var startGroup = groups[j];
                if (startGroup.path.Count == 0) continue;
                var path = startGroup.path;

                var endGroup = groups.Find((x) => x.number == path[path.Count - 1]);
                for (var i = -1; i < path.Count - 1; i++)
                {
                    MyLine myLine = null;
                    if (i == -1)
                    {
                        myLine = new MyLine(myCanvas, startGroup, null,
                            null, stopPoints.Find((x) => x.number == path[0]), getLineId());
                    }
                    else if (i != path.Count - 2)
                    {
                        myLine = new MyLine(myCanvas, null, null,
                            stopPoints.Find((x) => x.number == path[i]),
                            stopPoints.Find((x) => x.number == path[i + 1]), getLineId());
                    }
                    else
                    {
                        myLine = new MyLine(myCanvas, null, endGroup,
                            stopPoints.Find((x) => x.number == path[path.Count - 2]), null, getLineId());
                    }
                    if (!lines.Exists(x =>
                        x.startPoint == myLine.startPoint &&
                        x.endPoint == myLine.endPoint &&
                        x.startGr == myLine.startGr &&
                        x.endGr == myLine.endGr ||
                        x.startPoint == myLine.endPoint &&
                        x.endPoint == myLine.startPoint &&
                        x.startGr == myLine.endGr &&
                        x.endGr == myLine.startGr)
                        )
                    {
                        lines.Add(myLine);
                    } else
                    {
                        if (myLine.line != null)
                        {
                            myCanvas.Children.Remove(myLine.line);
                            myCanvas.Children.Remove(myLine.textBlock);
                        }
                    }
                }
            }

            foreach (var line in oldLines)
            {
                var a = lines.FindAll(x =>
                    x.startPoint == line[0] &&
                    x.endPoint == line[1] &&
                    x.startGr == line[2] &&
                    x.endGr == line[3]
                );
                if (a.Count != 0)
                {
                    a[0].lengthInSm = (int)line[4];
                    a[0].refreshLength();
                }
            }

            myCanvas.UpdateLayout();
        }

        private void deleteStopPointButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var g in groups)
            {
                for (int i = 0; i < g.path.Count - 1; i++)
                {
                    if (g.path[i] == selectedStopPoint.number)
                    {
                        MessageBox.Show("Невозможно удалить, так как точка входит в определенный путь. Сперва уберите точку из всех маршрутов.", 
                            "Невозможно удалить");
                        return;
                    }
                }
            }

            stopPoints.Remove(selectedStopPoint);
            myCanvas.Children.Remove(selectedStopPoint.grid);
            myCanvas.Children.Remove(selectedStopPoint.delayText);
            stopPointPanel.Visibility = Visibility.Collapsed;
            selectedStopPoint = null;
        }

        private void deleteGroupButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var g in groups)
            {
                if (g.path.Count == 0) continue;

                var lastP = g.path[g.path.Count - 1];
                if (g != selectedGroup && lastP == selectedGroup.number)
                {
                    MessageBox.Show("Невозможно удалить, так как группа является финальной точкой определенного маршрута.",
                        "Невозможно удалить");
                    return;
                }
            }

            groups.Remove(selectedGroup);
            myCanvas.Children.Remove(selectedGroup.grid);
            groupPanel.Visibility = Visibility.Collapsed;
            selectedGroup = null;

            recreateLines();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            isStarted = !isStarted;
            startButton.Content = isStarted ? "Остановить" : "Начать";

            if (isStarted) startJob(); else stopJob();
        }

        int curIndex = 0;
        private void stopJob()
        {
            timer.Stop();
            moveTasks.Clear();
            foreach (var model in models)
            {
                myCanvas.Children.Remove(model.shape);
            }
            models.Clear();
            curIndex = 0;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            long currentTimeInMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            updateTimer(currentTimeInMs);
            moveObjects(currentTimeInMs);
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

        private void moveObjects(long currentTimeInMs)
        {
            var top = 1L;
            var bot = 1L;
            if (speedMult < 1.1)
            {
                bot = 1; top = 1;
            }
            else if (speedMult < 1.9)
            {
                top = 2; bot = 3;
            }
            else if (speedMult < 2.9)
            {
                top = 1; bot = 2;
            }
            else
            {
                top = 1; bot = 3;
            }

            while (curIndex < moveTasks.Count && 
                moveTasks[curIndex].timeMs * top + startMs * bot <= currentTimeInMs * bot)
            {
                var model = models.Find(m => m.number == moveTasks[curIndex].modelNumber);
                model.x = moveTasks[curIndex].newX;
                model.y = moveTasks[curIndex].newY;
                model.redraw();
                curIndex++;
            }
            if (curIndex == moveTasks.Count)
            {
                startButton_Click(null, null);
                return;
            }
        }

        long startMs = -1;
        private void startJob()
        {
            if (modelsSpeedTextBox.Text.Length == 0 || int.Parse(modelsSpeedTextBox.Text) == 0)
            {
                MessageBox.Show("Задайте скорость моделей.", "Невозможно начать");
                startButton_Click(null, null);
                return;
            }
            if (groups.Count == 0)
            {
                MessageBox.Show("Создайте хотя бы один выход для моделей.", "Невозможно начать");
                startButton_Click(null, null);
                return;
            }
            lastSec = -1;
            startMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            calcAllPath();

            timerLabel.Text = "Время: 00:00";
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
                    var newModel = new Model(myCanvas, getModelId());
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
                            line = findLine(null, p1, p2, null);
                            d = p2.delayInMs;
                        }
                        else if (j == g.path.Count - 2)
                        {
                            var p1 = stopPoints.Find(p => p.number == g.path[g.path.Count - 2]);
                            var p2 = groups.Find(g1 => g1.number == g.path[g.path.Count - 1]);

                            x1 = p1.x; y1 = p1.y; x2 = p2.x; y2 = p2.y;
                            line = findLine(p1, null, null, p2);
                        }
                        else
                        {
                            var p1 = stopPoints.Find(p => p.number == g.path[j]);
                            var p2 = stopPoints.Find(p => p.number == g.path[j + 1]);

                            x1 = p1.x; y1 = p1.y; x2 = p2.x; y2 = p2.y;

                            line = findLine(p1, null, p2, null);
                            d = p2.delayInMs;
                        }

                        long endTimeInMs = startPathTimeInMs + (long)(line.lengthInSm /
                            (float)int.Parse(modelsSpeedTextBox.Text)) * 1000;
                        //Console.WriteLine("StartTime: " + startPathTimeInMs);
                        //Console.WriteLine("EndTime: " + endTimeInMs);
                        var totalFps = FPS * ((endTimeInMs - startPathTimeInMs) / 1000);
                        for (long k = 0; k <= totalFps; k++)
                        {
                            var newX = x1 + (k * (x2 - x1)) / totalFps + STOP_POINT_SIZE / 2;
                            var newY = y1 + (k * (y2 - y1)) / totalFps + STOP_POINT_SIZE / 2;
                            var newTime = startPathTimeInMs + (k * (endTimeInMs - startPathTimeInMs)) / totalFps;
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

        private MyLine? findLine(StopPoint startP, Group startG, StopPoint endP, Group endG)
        {
            return lines.Find(x =>
                x.startPoint == startP &&
                        x.endPoint == endP &&
                        x.startGr == startG &&
                        x.endGr == endG ||
                        x.startPoint == endP &&
                        x.endPoint == startP &&
                        x.startGr == endG &&
                        x.endGr == startG);               
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
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

            var line3 = modelsSpeedTextBox.Text.Length == 0 ? 0 : int.Parse(modelsSpeedTextBox.Text);
            res = line1 + "\n" + line2 + "\n" + line3;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Succub data (*.succubdata)|*.succubdata";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName.ToString());
                file.WriteLine(res);
                file.Close();
            }
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл";
            openFileDialog.Filter = "Succub data (*.succubdata)|*.succubdata";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
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
            foreach (StopPoint s in stopPoints)
            {
                myCanvas.Children.Remove(s.grid);
                myCanvas.Children.Remove(s.delayText);
            }
            foreach (var g in groups)
            {
                myCanvas.Children.Remove(g.grid);
            }
            foreach (var l in lines)
            {
                myCanvas.Children.Remove(l.line);
                myCanvas.Children.Remove(l.textBlock);
            }
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

                    var sp = new StopPoint(x, y, number, myCanvas);
                    sp.delayInMs = delayInMs;
                    stopPoints.Add(sp);
                    sp.redraw();
                    sp.refreshDelay();
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

                    var gr = new Group(x, y, number, myCanvas);
                    gr.delayFromStartInMs = delayFromStartInMs;
                    gr.modelsNumber = modelsNumber;
                    gr.periodInMs = periodInMs;
                    gr.path = path;
                    groups.Add(gr);
                    gr.redraw();
                }
            }

            var line3 = lines_[2];
            modelsSpeedTextBox.Text = line3;

            recreateLines();
        }

        private void lineEditTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void modelsSpeedTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }






    class StopPoint
    {
        public int delayInMs = 0;

        public int x;
        public int y;
        public int number;
        public Grid grid;
        public TextBlock delayText;

        public StopPoint(int x, int y, int number, Canvas canvas)
        {
            this.x = x; this.y = y; this.number = number;

            var grid = new Grid();
            var rect = new Rectangle()
            {
                Width = MainWindow.STOP_POINT_SIZE,
                Height = MainWindow.STOP_POINT_SIZE,
                Fill = new SolidColorBrush(Colors.White),
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 2,
            };
            var text = new TextBlock()
            {
                Text = number.ToString() + (delayInMs == 0 ? "" : "(" + (delayInMs / 1000) + ")"),
                FontSize = 17,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            delayText = new TextBlock()
            {
                Text = (delayInMs == 0 ? "" : "(" + (delayInMs / 1000) + "с.)"),
                FontSize = 17,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            grid.Children.Add(rect);
            grid.Children.Add(text);
            canvas.Children.Add(grid);
            canvas.Children.Add(delayText);
            this.grid = grid;
            redraw();
            Canvas.SetZIndex(grid, 2);
        }

        internal void redraw()
        {
            Canvas.SetLeft(grid, x);
            Canvas.SetTop(grid, y);

            Canvas.SetLeft(delayText, x + MainWindow.STOP_POINT_SIZE - 3);
            Canvas.SetTop(delayText, y + MainWindow.STOP_POINT_SIZE - 3);
        }

        public void refreshDelay()
        {
            delayText.Text = (delayInMs == 0 ? "" : "(" + (delayInMs / 1000) + "с.)");
        }
    }

    class Group
    {
        public int delayFromStartInMs = 0;
        public int modelsNumber = 1;
        public int periodInMs = 1000;
        public List<int> path = new List<int>();

        public int x;
        public int y;
        public int number;
        public Grid grid;

        public Group(int x, int y, int number, Canvas canvas)
        {
            this.x = x; this.y = y; this.number = number;

            var grid = new Grid();
            var rect = new Rectangle()
            {
                Width = MainWindow.START_POINT_SIZE,
                Height = MainWindow.START_POINT_SIZE,
                Stroke = new SolidColorBrush(Colors.Red),
                Fill = new SolidColorBrush(Colors.White),
                StrokeThickness = 2,
            };
            var text = new TextBlock()
            {
                Text = number.ToString(),
                FontSize = 17,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            grid.Children.Add(rect);
            grid.Children.Add(text);
            canvas.Children.Add(grid);
            this.grid = grid;
            Canvas.SetZIndex(grid, 2);

            redraw();
        }

        internal void redraw()
        {
            Canvas.SetLeft(grid, x);
            Canvas.SetTop(grid, y);
        }
    }

    class MyLine
    {
        int pointDelta = MainWindow.STOP_POINT_SIZE / 2;
        int groupDelta = MainWindow.START_POINT_SIZE / 2;

        public int number;

        public int lengthInSm = 300;

        public Canvas myCanvas;
        public Group startGr;
        public Group endGr;
        public StopPoint startPoint;
        public StopPoint endPoint;

        public Line line = null;
        public TextBlock textBlock = null;


        public MyLine(Canvas myCanvas,
            Group startGr, Group endGr, 
            StopPoint startPoint, StopPoint endPoint,
            int number)
        {
            this.myCanvas = myCanvas;
            this.startGr = startGr;
            this.endGr = endGr;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.number = number;

            refresh();
        }

        public void refresh()
        {
            if (line == null)
            {
                line = new Line();
                line.Visibility = Visibility.Visible;
                myCanvas.Children.Add(line);

                textBlock = new TextBlock()
                {
                    Text = number.ToString() + "(" + ((float)lengthInSm / 100) + "м)",
                    FontSize = 18,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Colors.Black)
                };
                Canvas.SetZIndex(textBlock, 2);
                myCanvas.Children.Add(textBlock);
            }

            line.StrokeThickness = 2;
            line.Stroke = Brushes.Black;
            double x1, y1, x2, y2;
            if (startPoint != null)
            {
                x1 = Canvas.GetLeft(startPoint.grid) + pointDelta;
                y1 = Canvas.GetTop(startPoint.grid) + pointDelta;
            }
            else
            {
                x1 = Canvas.GetLeft(startGr.grid) + groupDelta;
                y1 = Canvas.GetTop(startGr.grid) + groupDelta;
            }

            if (endPoint != null)
            {
                x2 = Canvas.GetLeft(endPoint.grid) + pointDelta;
                y2 = Canvas.GetTop(endPoint.grid) + pointDelta;
            }
            else
            {
                x2 = Canvas.GetLeft(endGr.grid) + groupDelta;
                y2 = Canvas.GetTop(endGr.grid) + groupDelta;
            }

            Canvas.SetZIndex(line, 1);
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;

            Canvas.SetLeft(textBlock, (x1 + x2) / 2);
            Canvas.SetTop(textBlock, (y1 + y2) / 2);
        }

        internal void refreshLength()
        {
            textBlock.Text = number.ToString() + "(" + ((float)lengthInSm / 100) + "м)";
        }
    }

    class Model
    {
        public int x = -1;
        public int y = -1;
        public int number;
        private Canvas myCanvas;
        public Border shape;

        public Model(Canvas myCanvas, int number)
        {
            this.myCanvas = myCanvas;
            this.number = number;
        }

        public void redraw()
        {
            var radius = 12.5f;
            if (shape == null)
            {
                var textBlock = new TextBlock()
                {
                    Text = number.ToString(),
                    FontSize = 18,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = SystemColors.ControlBrush,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                shape = new Border()
                {
                    Width = 25,
                    Height = 25,
                    Background = new SolidColorBrush(Colors.IndianRed),
                    CornerRadius = new CornerRadius(50),
                };
                shape.Child = textBlock;
                Canvas.SetZIndex(shape, 3);

                myCanvas.Children.Add(shape);
            }
            if (x == -1 || y == -1)
            {
                shape.Visibility = Visibility.Collapsed;
            }

            Canvas.SetLeft(shape, x - radius);
            Canvas.SetTop(shape, y - radius);
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
}
