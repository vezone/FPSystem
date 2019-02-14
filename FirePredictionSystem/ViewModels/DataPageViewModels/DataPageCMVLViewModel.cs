using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

using FirePredictionSystem.Additional;
using FirePredictionSystem.Commands;
using FirePredictionSystem.Models;

namespace FirePredictionSystem.ViewModels.DataPageViewModels
{
    class DataPageCMVLViewModel : ViewModelBase
    {
        //user stuff
        private string tb_MainTextBlock = "No table is created or loaded!";
        private string tb_PathToDownload;
        private List<Attribute> is_GridSource;

        public string MainTextBlock
        {
            get
            {
                return tb_MainTextBlock;
            }
            set
            {
                tb_MainTextBlock = value;
                OnPropertyChanged();
            }
        }

        public string PathToDownload
        {
            get
            {
                return tb_PathToDownload;
            }
            set
            {
                tb_PathToDownload = value;
                OnPropertyChanged();
            }
        }

        public List<Attribute> GridSource
        {
            get
            {
                return is_GridSource;
            }
            set
            {
                is_GridSource = value;
                OnPropertyChanged();
            }
        }

        public DataPageCMVLViewModel()
        {
            //.ctor
        }

        public RelayCommand btn_LoadTable_Click
        {
            get
            {
                return new RelayCommand(
                    (object obj) => 
                {
                    if (System.IO.File.Exists(PathToDownload))
                    {
                        //Loading table
                        string[][] table = IOClass.ReadTable(PathToDownload);
                        GridSource = new List<Attribute>();

                        int rLength = table.Length;
                        for (int r = 0; r < rLength; r++)
                        {
                            GridSource.Add(new Attribute(table[r]));
                        }
                        
                        MessageBox.Show("File is loaded!", "Success", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Invalid file path, use only valid file path!", "Path error", MessageBoxButton.OK);
                    }
                });
            }
        }

        public RelayCommand btn_FileSearch_Click
        {
            get
            {
                return new RelayCommand(
                    (object obj) => 
                {
                    //
                    Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        PathToDownload = openFileDialog.FileName;
                    }
                });
            }
        }

        public RelayCommand btn_Generate_Click
        {
            get
            {
                return new RelayCommand(
                    (object obj) =>
                {
                    //Generating data
                    
                });
            }
        }


    }
}
