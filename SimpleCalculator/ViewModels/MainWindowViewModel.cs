using SimpleCalculator.Commands;
using SimpleCalculator.Models;
using SimpleJson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using static SimpleCalculator.Properties.Resources;

namespace SimpleCalculator.ViewModels
{
    class MainWindowViewModel : NotificationObject
    {
        public ICommand SaveResultCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand CopyItemValueCommand { get; set; }
        public ICommand ClearResultsCommand { get; set; }
        public ICommand ShowAboutCommand { get; set; }
        public ICommand ViewSourceCommand { get; set; }
        public ICommand WindowClosingCommand { get; set; }
        public ICommand ToggleRestoreResultsAtStartupCommand { get; set; }
        public ICommand UpdateInputTextCommand { get; set; }

        private string _inputText;

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                RaisePropertyChanged("InputText");
            }
        }

        private string _result = "0";

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                RaisePropertyChanged("Result");
            }
        }

        private bool _restoreResultsAtStartup;

        public bool RestoreResultsAtStartup
        {
            get => _restoreResultsAtStartup;
            set
            {
                _restoreResultsAtStartup = value;
                RaisePropertyChanged("RestoreResultsAtStartup");
            }
        }

        private ObservableCollection<ResultItem> _resultItems;

        public ObservableCollection<ResultItem> ResultItems
        {
            get => _resultItems;
            set
            {
                _resultItems = value;
                RaisePropertyChanged("ResultItems");
            }
        }

        private void Calculate()
        {
            try
            {
                Result = string.IsNullOrWhiteSpace(InputText) ? "0" : Calculator.Calculate(InputText).ToString();
            }
            catch (Exception e)
            {
                Result = e.Message;
            }
        }

        private void SaveResult(object expression)
        {
            double result;
            try
            {
                result = Calculator.Calculate(expression.ToString());
            }
            catch
            {
                return;
            }
            if (expression is string expstr)
            {
                expstr = expstr.Trim();
                InputText = string.Empty;

                if (string.IsNullOrEmpty(expstr))
                    return;

                var item = new ResultItem()
                {
                    Result = result,
                    Experssion = FormatExpression(expstr)
                };

                if (ResultItems.Count > 0 && ResultItems[0] == item)
                    return;

                ResultItems.Insert(0, item);
            }
        }

        private string FormatExpression(string expression)
        {
            var sb = new StringBuilder();
            foreach (var item in expression)
            {
                if (char.IsWhiteSpace(item))
                    continue;
                sb.Append(item switch
                {
                    '*' => '×',
                    '/' => '÷',
                    _ => item
                });
            }
            return sb.ToString();
        }

        private void RemoveItem(object item)
        {
            if (item is ResultItem resultItem)
                ResultItems.Remove(resultItem);
        }

        private void CopyItemValue(object value)
        {
            if (value == null)
                return;

            var str = value is ResultItem item ? item.Result.ToString() : value.ToString();
            if (str != Clipboard.GetText())
                Clipboard.SetText(str);
        }

        private void ClearResults()
        {
            ResultItems.Clear();
        }

        private void ShowAbout()
        {
            MessageBox.Show($"SimpleCalculator v{Application.ResourceAssembly.GetName().Version} by Mzying2001", "About");
        }

        private void ViewSource()
        {
            Process.Start(new ProcessStartInfo("https://github.com/Mzying2001/SimpleCalculator")
            {
                UseShellExecute = true
            }).Dispose();
        }

        private void ToggleRestoreResultsAtStartup()
        {
            RestoreResultsAtStartup = !RestoreResultsAtStartup;
        }

        private void RestoreResults()
        {
            try
            {
                foreach (var item in JsonReader.ReadArrayFile<JObject>(RESULTS_JSON))
                    ResultItems.Add(JsonConvert.Deserialize<ResultItem>(item));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveResults()
        {
            try
            {
                JsonWriter.WriteArrayFile(RESULTS_JSON, (from item in ResultItems select JsonConvert.Serialize(item)).ToArray());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WindowClosing()
        {
            if (RestoreResultsAtStartup)
                SaveResults();
            else
            {
                if (System.IO.File.Exists(RESULTS_JSON))
                    System.IO.File.Delete(RESULTS_JSON);
            }
        }

        private void UpdateInputText(object input)
        {
            if (input is string str)
            {
                InputText = str;
                Calculate();
            }
            else if (input is ResultItem item)
            {
                var sb = new StringBuilder();
                foreach (var c in item.Experssion)
                {
                    sb.Append(c switch
                    {
                        '×' => '*',
                        '÷' => '/',
                        _ => c
                    });
                }
                UpdateInputText(sb.ToString());
            }
        }

        public MainWindowViewModel()
        {
            ResultItems = new ObservableCollection<ResultItem>();
            RestoreResultsAtStartup = System.IO.File.Exists(RESULTS_JSON);

            if (RestoreResultsAtStartup)
                RestoreResults();

            SaveResultCommand = new DelegateCommand(SaveResult);
            RemoveItemCommand = new DelegateCommand(RemoveItem);
            CopyItemValueCommand = new DelegateCommand(CopyItemValue);
            ClearResultsCommand = new DelegateCommand(ClearResults);
            ShowAboutCommand = new DelegateCommand(ShowAbout);
            ViewSourceCommand = new DelegateCommand(ViewSource);
            WindowClosingCommand = new DelegateCommand(WindowClosing);
            ToggleRestoreResultsAtStartupCommand = new DelegateCommand(ToggleRestoreResultsAtStartup);
            UpdateInputTextCommand = new DelegateCommand(UpdateInputText);
        }
    }
}
