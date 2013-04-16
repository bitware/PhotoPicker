﻿using System;
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
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace PhotoPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members
        MainViewModel _viewModel = null;
        private static string[] _imageTypes = new string[] {"jpg", "jpeg", "png"};
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            //  We have declared the view model instance declaratively in the xaml.
            //  Get the reference to it here, so we can use it in the button click event.
            _viewModel = (MainViewModel)base.DataContext;
        }

        private static List<string> allSupportedTypes()
        {
            List<string> types = new List<string>();

            foreach (string imageType in _imageTypes)
            {
                types.Add("*." + imageType);
            }

            return types;
        }

        private static string allSupportedTypesString()
        {
            return "All supported graphics" + "|" + string.Join(";", allSupportedTypes());
        }

        private static List<string> jpegTypes()
        {
            List<string> types = new List<string>();

            types.Add("*." + _imageTypes[0]);
            types.Add("*." + _imageTypes[1]);

            return types;
        }

        private static string jpegTypesString()
        {
            return "JPEG graphics" + "|" + string.Join(";", jpegTypes());
        }

        private static List<string> pngTypes()
        {
            List<string> types = new List<string>();

            types.Add("*." + _imageTypes[2]);

            return types;
        }

        private static string pngTypesString()
        {
            return "PNG graphics" + "|" + string.Join(";", pngTypes());
        }

        private static List<string> getTypes(int typesIndex)
        {
            switch (typesIndex)
            {
                case 0:
                    return allSupportedTypes();
                case 1:
                    return jpegTypes();
                case 2:
                    return pngTypes();
                default:
                    return null;
            }
        }

        private void OpenFileCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.Handled = true;
            e.CanExecute = true;
        }

        private void OpenFileExecuted(object sender, ExecutedRoutedEventArgs e) {
            e.Handled = true;

            OpenFileDialog op = new OpenFileDialog();

            op.Title = "Select a picture";
            op.Filter = allSupportedTypesString() + "|"
                + jpegTypesString() + "|"
                + pngTypesString();

            DialogResult result = op.ShowDialog();
            if (result.ToString() == "OK")
            {
                _viewModel.setImage(op.FileName, getTypes(op.FilterIndex - 1));
            }
        }

        private void PreviousCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.Handled = true;
            e.CanExecute = (_viewModel != null) && (_viewModel.Index > 0);
        }

        private void PreviousExecuted(object sender, ExecutedRoutedEventArgs e) {
            e.Handled = true;
            _viewModel.Index -= 1;
        }

        private void NextCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.Handled = true;
            e.CanExecute = (_viewModel != null) && (_viewModel.Index < _viewModel.Files.Length - 2);
        }

        private void NextExecuted(object sender, RoutedEventArgs e) {
            e.Handled = true;
            _viewModel.Index += 1;
        }

        private void HomeCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.Handled = true;
            e.CanExecute = (_viewModel != null) && (_viewModel.Index > 0);
        }

        private void HomeExecuted(object sender, RoutedEventArgs e) {
            e.Handled = true;
            _viewModel.Index = 0;
        }

        private void EndCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.Handled = true;
            e.CanExecute = (_viewModel != null) && (_viewModel.Index < _viewModel.Files.Length - 2);
        }

        private void EndExecuted(object sender, RoutedEventArgs e) {
            e.Handled = true;
            _viewModel.Index = _viewModel.Files.Length - 1;
        }

        private void HelpCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void HelpExecuted(object sender, ExecutedRoutedEventArgs e) {
            System.Windows.Forms.MessageBox.Show("Hey, I'm some help.");
            e.Handled = true;
        }
    }
}
