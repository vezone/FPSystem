namespace FirePredictionSystem.ViewModels
{
    using System.Windows;
    using System.Collections.Generic;

    using FirePredictionSystem.Models;
    using FirePredictionSystem.Commands;
    using FirePredictionSystem.Additional;
    using FirePredictionSystem.Additional.C45;

    class MainViewModel : ViewModelBase
    {
        private string m_ImagePath;
        private string m_FilePath;
        private List<Attribute> m_Data;
        private string[][] m_ReadedData;

        public string ImagePath
        {
            get
            {
                return m_ImagePath;
            }
            set
            {
                m_ImagePath = value;
                OnPropertyChanged();
            }
        }

        public string FilePath
        {
            get
            {
                return m_FilePath;
            }
            set
            {
                m_FilePath = value;
                OnPropertyChanged();
            }
        }

        public List<Attribute> Data
        {
            get
            {
                return m_Data;
            }
            set
            {
                m_Data = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            //set it like this for now
            ImagePath =
                "E:/General/Programming/C_Sharp/WPF/FirePredictionSystem/FirePredictionSystem/bin/Debug/Images/C45.png";
        }

        public RelayCommand btn_LoadFile_Click
        {
            get
            {
                return new RelayCommand(
                    (object obj) =>
                    {
                        if (FilePath != null)
                        {
                            if (System.IO.File.Exists(FilePath))
                            {
                                //Loading table
                                m_ReadedData = IOClass.ReadTable(FilePath);
                                Data = new List<Attribute>();

                                int rLength = m_ReadedData.Length;
                                for (int r = 0; r < rLength; r++)
                                {
                                    Data.Add(new Attribute(m_ReadedData[r]));
                                }

                                MessageBox.Show("File is loaded!", "Success", MessageBoxButton.OK);
                            }
                            else
                            {
                                MessageBox.Show("Invalid file path, use only valid file path!", 
                                    "Path error", 
                                    MessageBoxButton.OK);
                            }
                        }
                    });
            }
        }

        public RelayCommand btn_BrowseFile_Click
        {
            get
            {
                return new RelayCommand(
                    (object obj) =>
                    {
                        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                        if (openFileDialog.ShowDialog() == true)
                        {
                            FilePath = openFileDialog.FileName;
                        }
                    });
            }
        }

        public RelayCommand btn_Run_Click
        {
            get
            {
                return new RelayCommand(
                    (object obj) =>
                    {
                        //С4.5 stuff there
                        var input = new Input(m_ReadedData);
                        Tree tree = new Tree();
                        tree.BuildID3(input, 100);

                        var graph = new TreeGraph(tree);
                        graph.BuildGraph();
                        graph.DrawGraph("graph.dot");

                        System.Diagnostics.Process.Start(@"get_image_1.bat");
                        ImagePath =
                         "E:/General/Programming/C_Sharp/WPF/FirePredictionSystem/FirePredictionSystem/bin/Debug/Images/Output_C45.png";
                    });
            }
        }

    }
}
