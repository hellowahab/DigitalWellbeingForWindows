﻿using DigitalWellbeingWPF.Models.UserControls;
using DigitalWellbeingWPF.ViewModels;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DigitalWellbeingWPF.Views
{
    /// <summary>
    /// Interaction logic for DayAppUsagePage.xaml
    /// </summary>
    public partial class DayAppUsagePage : Page
    {
        AppUsageViewModel vm;

        public DayAppUsagePage()
        {
            InitializeComponent();

            vm = (AppUsageViewModel)DataContext;
        }

        private void BtnPreviousDay_Click(object sender, RoutedEventArgs e)
        {
            vm.LoadPreviousDay();
        }

        private void BtnNextDay_Click(object sender, RoutedEventArgs e)
        {
            vm.LoadNextDay();
        }

        private void appUsageChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            AppUsageListView.SelectedItem = vm.OnAppUsageChart_SelectionChanged(chartPoint);
            ModernWpf.Controls.ListViewItem item = (ModernWpf.Controls.ListViewItem)AppUsageListView.ItemContainerGenerator.ContainerFromItem(AppUsageListView.SelectedItem);
            item.Focus();
        }

        private void WeeklyChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            vm.WeeklyChart_SelectionChanged((int)chartPoint.X);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            vm.OnPageResize(appUsageChart.ActualWidth, appUsageChart.ActualHeight);
        }

        private void AppUsageListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string processName = ((MenuItem)sender).Tag.ToString();
            Properties.Settings.Default.UserExcludedProcesses.Add(processName);
            Properties.Settings.Default.Save();

            vm.OnExcludeApp(processName);
        }

        public void OnNavigate()
        {
            vm.OnNavigate();
        }
    }
}
