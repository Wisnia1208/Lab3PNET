using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace Lab3PNET
{
    public partial class MainWindow : Window
    {
        private RandomNumberGenerator _rng = new RandomNumberGenerator();
        private List<int> _randomNumbers = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NumberOfElementsTextBox.Text, out int count) &&
                int.TryParse(RangeFromTextBox.Text, out int minValue) &&
                int.TryParse(RangeToTextBox.Text, out int maxValue))
            {
                ProgressBar.Value = 0;
                StatusBarItem.Content = "Generating...";
                _randomNumbers = await _rng.Generate(count, minValue, maxValue, ProgressBar);
                DisplayNumbers(_randomNumbers);
                StatusBarItem.Content = "Ready";
            }
            else
            {
                MessageBox.Show("Please enter valid numbers.");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "random_numbers.txt";
            _rng.SaveToFile(_randomNumbers, filePath);
            MessageBox.Show($"Liczby zapisane do pliku: {filePath}");
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "random_numbers.txt";
            _randomNumbers = _rng.LoadFromFile(filePath);
            DisplayNumbers(_randomNumbers);
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the list and ListBox
            _randomNumbers.Clear();
            GeneratedNumbersListBox.Items.Clear();
            ProgressBar.Value = 0;
            StatusBarItem.Content = "Ready";

            // Optionally, you can also clear the input fields
            NumberOfElementsTextBox.Clear();
            RangeFromTextBox.Clear();
            RangeToTextBox.Clear();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void DisplayNumbers(List<int> numbers)
        {
            GeneratedNumbersListBox.Items.Clear();
            foreach (int number in numbers)
            {
                GeneratedNumbersListBox.Items.Add(number);
            }
        }
    }

    public class RandomNumberGenerator
    {
        private static readonly Random _random = new Random();

        public async Task<List<int>> Generate(int count, int minValue, int maxValue, ProgressBar progressBar)
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int number = _random.Next(minValue, maxValue + 1);
                numbers.Add(number);
                progressBar.Value = ((i + 1) * 100 / count);
                await Task.Delay(500);
            }
            return numbers;
        }

        public void SaveToFile(List<int> numbers, string filePath)
        {
            File.WriteAllLines(filePath, numbers.ConvertAll(n => n.ToString()));
        }

        public List<int> LoadFromFile(string filePath)
        {
            List<int> numbers = new List<int>();
            foreach (var line in File.ReadAllLines(filePath))
            {
                if (int.TryParse(line, out int number))
                {
                    numbers.Add(number);
                }
            }
            return numbers;
        }
    }
}
