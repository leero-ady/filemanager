using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security.AccessControl;



namespace myfirstwork
{
   
    
    public partial class Form1 : Form
    {

        public string myitems="";
        public string myString="";

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            listviewPartition();
        }

        public void listviewPartition()
        {
            listView1.Clear();
            listView1.Columns.Add("File", 270, HorizontalAlignment.Left);
            listView1.Columns.Add("Path", 360, HorizontalAlignment.Left);
            listView1.Columns.Add("Size", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Last Modified", 270, HorizontalAlignment.Center);

            //initial color
            fileName1.BackColor = Color.White;
            //initial color
            // Set the view to show details.
            listView1.View = View.Details;
            // Allow the user to edit item text.
            listView1.LabelEdit = true;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Display check boxes.
            listView1.CheckBoxes = false;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;
            // Sort the items in the list in ascending order.
            listView1.Sorting = SortOrder.Ascending;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            listView1.Clear();
            listviewPartition();
               
           //virtual progress
            int yy;
            this.Cursor = Cursors.WaitCursor;
           toolStripStatusLabel1.Text = "Ready";
           

            for (yy = 0; yy < 20; yy++)
            {
                
                progressBar1.Value = yy;
                Thread.Sleep(100);
            }
            //virtual progress


            //init
            label8.Text = "";
            textBox1.BackColor = Color.White;
            directory1.BackColor = Color.White;


            if (((fileName1.Text).Equals("") && !checkBox15.Checked) && !(checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || checkBox8.Checked || checkBox9.Checked || checkBox10.Checked || checkBox11.Checked || checkBox12.Checked || checkBox13.Checked || checkBox14.Checked))
            {
                label8.Text = "Enter File Name";
                fileName1.BackColor = Color.Tomato;

            }
            else if ((!(fileName1.Text).Equals("") && !checkBox15.Checked) && !(checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || checkBox8.Checked || checkBox9.Checked || checkBox10.Checked || checkBox11.Checked || checkBox12.Checked || checkBox13.Checked || checkBox14.Checked))
            {
                label8.Text = "Please Select Any Extension";
                fileName1.BackColor = Color.Tomato;
            }


            //init
            
            
            String fileNameToFind1;
            if (!((fileName1.Text).Equals("")))
                fileNameToFind1 = "*" + fileName1.Text + "*";
            else
                fileNameToFind1 = "*.*";
            String directory = directory1.Text;
            String destname;

            if (directory.Equals("E:\\"))
            {
                directory = "E:\\downloads";
            }

            //acescs rights
           // AddDirectorySecurity(directory, @"Users", FileSystemRights.ReadData, AccessControlType.Allow);
            //access rights
            try
            {
                ListViewItem.ListViewSubItem lvsi;
                backgroundWorker1.RunWorkerAsync();
                 destname = textBox2.Text;
                destname = destname + "\\Repository";
                if (!Directory.Exists(destname))
                {
                    Directory.CreateDirectory(destname);
                }

                //dynamic directory

                String[] files = Directory.GetFiles(directory, fileNameToFind1, SearchOption.AllDirectories);
                String[] fn = new String[files.Length];
                long[] fs = new long[files.Length];
                for (int iFile = 0; iFile < files.Length; iFile++)
                {
                    fn[iFile] = new FileInfo(files[iFile]).Name;
                    fs[iFile] = new FileInfo(files[iFile]).Length;
                }
                myString = (files.Length).ToString();
           
                //convert
               
                //convert

                //size
                double size;
                long factor = 0;
                if (comboBox1.SelectedIndex == 0)
                {
                    factor = Convert.ToInt64(1048576);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    factor = Convert.ToInt64(1073741824);
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    factor = Convert.ToInt64(1024);
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    factor = Convert.ToInt64(1);
                }
                if (!(textBox1.Text).Equals(""))
                {
                    size = Convert.ToDouble(textBox1.Text) * factor;

               }
                else
                {
                    size = 0;
                    
                }
                //size
                // f in fn

                ListViewItem[] item1 = new ListViewItem[files.Length];
                for (int iFile = 0; iFile < files.Length; iFile++)
                {
                    Double p12 = files.Length;
                    Double prog = (((Double)iFile) / p12) * 100.0;
                    progressBar1.Value = (int)prog + 20;
                    if(iFile == files.Length-1)
                        progressBar1.Value = 120;

                    if (size == 0)
                    {
                        

                        if (((fn[iFile].EndsWith(".PNG") || fn[iFile].EndsWith(".png")) && checkBox5.Checked )|| ((fn[iFile].EndsWith(".PNG") || fn[iFile].EndsWith(".png"))&& checkBox15.Checked ) )
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 2);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".PDF") || fn[iFile].EndsWith(".pdf")) && checkBox2.Checked) || ((fn[iFile].EndsWith(".PDF") || fn[iFile].EndsWith(".pdf")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 1);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MP4") || fn[iFile].EndsWith(".mp4")) && checkBox4.Checked) || ((fn[iFile].EndsWith(".MP4") || fn[iFile].EndsWith(".mp4")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 4);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MP3") || fn[iFile].EndsWith(".mp3")) && checkBox3.Checked) || ((fn[iFile].EndsWith(".MP3") || fn[iFile].EndsWith(".mp3")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 3);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".JPG") || fn[iFile].EndsWith(".jpg")) && checkBox1.Checked) || ((fn[iFile].EndsWith(".JPG") || fn[iFile].EndsWith(".jpg")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 0);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MOV") || fn[iFile].EndsWith(".mov")) && checkBox11.Checked) || ((fn[iFile].EndsWith(".MOV") || fn[iFile].EndsWith(".mov")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 5);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".FLV") || fn[iFile].EndsWith(".flv")) && checkBox6.Checked) || ((fn[iFile].EndsWith(".FLV") || fn[iFile].EndsWith(".flv")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 6);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".EXE") || fn[iFile].EndsWith(".exe")) && checkBox13.Checked) || ((fn[iFile].EndsWith(".EXE") || fn[iFile].EndsWith(".exe")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 7);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".TXT") || fn[iFile].EndsWith(".txt")) && checkBox7.Checked) || ((fn[iFile].EndsWith(".TXT") || fn[iFile].EndsWith(".txt")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 8);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".DOCX") || fn[iFile].EndsWith(".docx")) && checkBox8.Checked) || ((fn[iFile].EndsWith(".DOCX") || fn[iFile].EndsWith(".docx")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 9);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".PPT") || fn[iFile].EndsWith(".ppt")) && checkBox9.Checked) || ((fn[iFile].EndsWith(".PPT") || fn[iFile].EndsWith(".ppt")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 10);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".HTML") || fn[iFile].EndsWith(".html")) && checkBox10.Checked) || ((fn[iFile].EndsWith(".HTML") || fn[iFile].EndsWith(".html")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 11);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".MKV") || fn[iFile].EndsWith(".mkv")) && checkBox12.Checked) || ((fn[iFile].EndsWith(".MKV") || fn[iFile].EndsWith(".mkv")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 12);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".ZIP") || fn[iFile].EndsWith(".zip")) && checkBox14.Checked) || ((fn[iFile].EndsWith(".ZIP") || fn[iFile].EndsWith(".zip")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 13);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if(checkBox15.Checked)
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 14);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                    }
                    if (0 < size && size <= 1024 && (size - 10 * 128 < fs[iFile] && fs[iFile] < size + 10 * 128))
                    {


                        if (((fn[iFile].EndsWith(".PNG") || fn[iFile].EndsWith(".png")) && checkBox5.Checked) || ((fn[iFile].EndsWith(".PNG") || fn[iFile].EndsWith(".png")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 2);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".PDF") || fn[iFile].EndsWith(".pdf")) && checkBox2.Checked) || ((fn[iFile].EndsWith(".PDF") || fn[iFile].EndsWith(".pdf")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 1);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MP4") || fn[iFile].EndsWith(".mp4")) && checkBox4.Checked) || ((fn[iFile].EndsWith(".MP4") || fn[iFile].EndsWith(".mp4")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 4);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MP3") || fn[iFile].EndsWith(".mp3")) && checkBox3.Checked) || ((fn[iFile].EndsWith(".MP3") || fn[iFile].EndsWith(".mp3")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 3);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".JPG") || fn[iFile].EndsWith(".jpg")) && checkBox1.Checked) || ((fn[iFile].EndsWith(".JPG") || fn[iFile].EndsWith(".jpg")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 0);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MOV") || fn[iFile].EndsWith(".mov")) && checkBox11.Checked) || ((fn[iFile].EndsWith(".MOV") || fn[iFile].EndsWith(".mov")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 5);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".FLV") || fn[iFile].EndsWith(".flv")) && checkBox6.Checked) || ((fn[iFile].EndsWith(".FLV") || fn[iFile].EndsWith(".flv")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 6);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".EXE") || fn[iFile].EndsWith(".exe")) && checkBox13.Checked) || ((fn[iFile].EndsWith(".EXE") || fn[iFile].EndsWith(".exe")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 7);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".TXT") || fn[iFile].EndsWith(".txt")) && checkBox7.Checked) || ((fn[iFile].EndsWith(".TXT") || fn[iFile].EndsWith(".txt")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 8);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".DOCX") || fn[iFile].EndsWith(".docx")) && checkBox8.Checked) || ((fn[iFile].EndsWith(".DOCX") || fn[iFile].EndsWith(".docx")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 9);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".PPT") || fn[iFile].EndsWith(".ppt")) && checkBox9.Checked) || ((fn[iFile].EndsWith(".PPT") || fn[iFile].EndsWith(".ppt")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 10);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".HTML") || fn[iFile].EndsWith(".html")) && checkBox10.Checked) || ((fn[iFile].EndsWith(".HTML") || fn[iFile].EndsWith(".html")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 11);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".MKV") || fn[iFile].EndsWith(".mkv")) && checkBox12.Checked) || ((fn[iFile].EndsWith(".MKV") || fn[iFile].EndsWith(".mkv")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 12);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".ZIP") || fn[iFile].EndsWith(".zip")) && checkBox14.Checked) || ((fn[iFile].EndsWith(".ZIP") || fn[iFile].EndsWith(".zip")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 13);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (checkBox15.Checked)
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 14);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                    }
                    if (1024 < size && size <= 1048576 && (size - 100 * 1024 < fs[iFile] && fs[iFile] < size + 100 * 1024))
                    {

                        if (((fn[iFile].EndsWith(".PNG") || fn[iFile].EndsWith(".png")) && checkBox5.Checked) || ((fn[iFile].EndsWith(".PNG") || fn[iFile].EndsWith(".png")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 2);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".PDF") || fn[iFile].EndsWith(".pdf")) && checkBox2.Checked) || ((fn[iFile].EndsWith(".PDF") || fn[iFile].EndsWith(".pdf")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 1);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MP4") || fn[iFile].EndsWith(".mp4")) && checkBox4.Checked) || ((fn[iFile].EndsWith(".MP4") || fn[iFile].EndsWith(".mp4")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 4);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MP3") || fn[iFile].EndsWith(".mp3")) && checkBox3.Checked) || ((fn[iFile].EndsWith(".MP3") || fn[iFile].EndsWith(".mp3")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 3);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".JPG") || fn[iFile].EndsWith(".jpg")) && checkBox1.Checked) || ((fn[iFile].EndsWith(".JPG") || fn[iFile].EndsWith(".jpg")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 0);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MOV") || fn[iFile].EndsWith(".mov")) && checkBox11.Checked) || ((fn[iFile].EndsWith(".MOV") || fn[iFile].EndsWith(".mov")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 5);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".FLV") || fn[iFile].EndsWith(".flv")) && checkBox6.Checked) || ((fn[iFile].EndsWith(".FLV") || fn[iFile].EndsWith(".flv")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 6);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".EXE") || fn[iFile].EndsWith(".exe")) && checkBox13.Checked) || ((fn[iFile].EndsWith(".EXE") || fn[iFile].EndsWith(".exe")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 7);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".TXT") || fn[iFile].EndsWith(".txt")) && checkBox7.Checked) || ((fn[iFile].EndsWith(".TXT") || fn[iFile].EndsWith(".txt")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 8);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".DOCX") || fn[iFile].EndsWith(".docx")) && checkBox8.Checked) || ((fn[iFile].EndsWith(".DOCX") || fn[iFile].EndsWith(".docx")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 9);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".PPT") || fn[iFile].EndsWith(".ppt")) && checkBox9.Checked) || ((fn[iFile].EndsWith(".PPT") || fn[iFile].EndsWith(".ppt")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 10);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".HTML") || fn[iFile].EndsWith(".html")) && checkBox10.Checked) || ((fn[iFile].EndsWith(".HTML") || fn[iFile].EndsWith(".html")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 11);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".MKV") || fn[iFile].EndsWith(".mkv")) && checkBox12.Checked) || ((fn[iFile].EndsWith(".MKV") || fn[iFile].EndsWith(".mkv")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 12);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".ZIP") || fn[iFile].EndsWith(".zip")) && checkBox14.Checked) || ((fn[iFile].EndsWith(".ZIP") || fn[iFile].EndsWith(".zip")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 13);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (checkBox15.Checked)
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 14);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                    }
                    if (1048576 < size && size <= 1073741824 && (size - 10 * 1048576 < fs[iFile] && fs[iFile] < size + 10 * 1048576))
                    {

                        if (((fn[iFile].EndsWith(".PNG") || fn[iFile].EndsWith(".png")) && checkBox5.Checked) || ((fn[iFile].EndsWith(".PNG") || fn[iFile].EndsWith(".png")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 2);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".PDF") || fn[iFile].EndsWith(".pdf")) && checkBox2.Checked) || ((fn[iFile].EndsWith(".PDF") || fn[iFile].EndsWith(".pdf")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 1);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MP4") || fn[iFile].EndsWith(".mp4")) && checkBox4.Checked) || ((fn[iFile].EndsWith(".MP4") || fn[iFile].EndsWith(".mp4")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 4);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MP3") || fn[iFile].EndsWith(".mp3")) && checkBox3.Checked) || ((fn[iFile].EndsWith(".MP3") || fn[iFile].EndsWith(".mp3")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 3);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".JPG") || fn[iFile].EndsWith(".jpg")) && checkBox1.Checked) || ((fn[iFile].EndsWith(".JPG") || fn[iFile].EndsWith(".jpg")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 0);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MOV") || fn[iFile].EndsWith(".mov")) && checkBox11.Checked) || ((fn[iFile].EndsWith(".MOV") || fn[iFile].EndsWith(".mov")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 5);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".FLV") || fn[iFile].EndsWith(".flv")) && checkBox6.Checked) || ((fn[iFile].EndsWith(".FLV") || fn[iFile].EndsWith(".flv")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 6);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".EXE") || fn[iFile].EndsWith(".exe")) && checkBox13.Checked) || ((fn[iFile].EndsWith(".EXE") || fn[iFile].EndsWith(".exe")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 7);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".TXT") || fn[iFile].EndsWith(".txt")) && checkBox7.Checked) || ((fn[iFile].EndsWith(".TXT") || fn[iFile].EndsWith(".txt")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 8);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".DOCX") || fn[iFile].EndsWith(".docx")) && checkBox8.Checked) || ((fn[iFile].EndsWith(".DOCX") || fn[iFile].EndsWith(".docx")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 9);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".PPT") || fn[iFile].EndsWith(".ppt")) && checkBox9.Checked) || ((fn[iFile].EndsWith(".PPT") || fn[iFile].EndsWith(".ppt")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 10);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".HTML") || fn[iFile].EndsWith(".html")) && checkBox10.Checked) || ((fn[iFile].EndsWith(".HTML") || fn[iFile].EndsWith(".html")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 11);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".MKV") || fn[iFile].EndsWith(".mkv")) && checkBox12.Checked) || ((fn[iFile].EndsWith(".MKV") || fn[iFile].EndsWith(".mkv")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 12);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".ZIP") || fn[iFile].EndsWith(".zip")) && checkBox14.Checked) || ((fn[iFile].EndsWith(".ZIP") || fn[iFile].EndsWith(".zip")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 13);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (checkBox15.Checked)
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 14);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                    }
                    if (1073741824 < size  && (size - 500 * 1048576 < fs[iFile] && fs[iFile] < size + 500 * 1048576))
                    {

                        if (((fn[iFile].EndsWith(".PNG") || fn[iFile].EndsWith(".png")) && checkBox5.Checked) || ((fn[iFile].EndsWith(".PNG") || fn[iFile].EndsWith(".png")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 2);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".PDF") || fn[iFile].EndsWith(".pdf")) && checkBox2.Checked) || ((fn[iFile].EndsWith(".PDF") || fn[iFile].EndsWith(".pdf")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 1);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MP4") || fn[iFile].EndsWith(".mp4")) && checkBox4.Checked) || ((fn[iFile].EndsWith(".MP4") || fn[iFile].EndsWith(".mp4")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 4);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MP3") || fn[iFile].EndsWith(".mp3")) && checkBox3.Checked) || ((fn[iFile].EndsWith(".MP3") || fn[iFile].EndsWith(".mp3")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 3);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".JPG") || fn[iFile].EndsWith(".jpg")) && checkBox1.Checked) || ((fn[iFile].EndsWith(".JPG") || fn[iFile].EndsWith(".jpg")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 0);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".MOV") || fn[iFile].EndsWith(".mov")) && checkBox11.Checked) || ((fn[iFile].EndsWith(".MOV") || fn[iFile].EndsWith(".mov")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 5);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".FLV") || fn[iFile].EndsWith(".flv")) && checkBox6.Checked) || ((fn[iFile].EndsWith(".FLV") || fn[iFile].EndsWith(".flv")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 6);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".EXE") || fn[iFile].EndsWith(".exe")) && checkBox13.Checked) || ((fn[iFile].EndsWith(".EXE") || fn[iFile].EndsWith(".exe")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 7);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".TXT") || fn[iFile].EndsWith(".txt")) && checkBox7.Checked) || ((fn[iFile].EndsWith(".TXT") || fn[iFile].EndsWith(".txt")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 8);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".DOCX") || fn[iFile].EndsWith(".docx")) && checkBox8.Checked) || ((fn[iFile].EndsWith(".DOCX") || fn[iFile].EndsWith(".docx")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 9);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".PPT") || fn[iFile].EndsWith(".ppt")) && checkBox9.Checked) || ((fn[iFile].EndsWith(".PPT") || fn[iFile].EndsWith(".ppt")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 10);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".HTML") || fn[iFile].EndsWith(".html")) && checkBox10.Checked) || ((fn[iFile].EndsWith(".HTML") || fn[iFile].EndsWith(".html")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 11);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }

                        else if (((fn[iFile].EndsWith(".MKV") || fn[iFile].EndsWith(".mkv")) && checkBox12.Checked) || ((fn[iFile].EndsWith(".MKV") || fn[iFile].EndsWith(".mkv")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 12);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (((fn[iFile].EndsWith(".ZIP") || fn[iFile].EndsWith(".zip")) && checkBox14.Checked) || ((fn[iFile].EndsWith(".ZIP") || fn[iFile].EndsWith(".zip")) && checkBox15.Checked))
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 13);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                        else if (checkBox15.Checked)
                        {
                            FileStream name = File.Create(destname + "\\" + fn[iFile]);
                            name.Close();
                            File.Copy(files[iFile], destname + "\\" + fn[iFile], true);
                            //ListViewItem[] item1 = new ListViewItem[iFile];
                            item1[iFile] = new ListViewItem(fn[iFile], 14);
                            item1[iFile].SubItems.Add(fn[iFile] = new FileInfo(files[iFile]).FullName);
                            item1[iFile].SubItems.Add(((float)fs[iFile] / 1000.0) + "KB".ToString());//= new FileInfo(files[iFile]).Length);
                            lvsi = new ListViewItem.ListViewSubItem();
                            lvsi.Text = (new FileInfo(fn[iFile]).LastAccessTime.ToString());
                            item1[iFile].SubItems.Add(lvsi);
                            listView1.Items.AddRange(new ListViewItem[] { item1[iFile] });
                            //listView1.Items.Add(fn[iFile], 2);
                        }
                    }
                }
                int itemms=listView1.Items.Count;
                this.Cursor = Cursors.Default;
                 myitems = (itemms).ToString();
                 progressBar1.Value = 120; 
                if (!myString.Equals(""))
                    toolStripStatusLabel1.Text = "Files Scanned : " + myString + "   Files Retrieved : " + myitems;
                else
                    toolStripStatusLabel1.Text = "No Results";  

                

            }
            catch (ArgumentException)
            {
                progressBar1.Value = 120;
                this.Cursor = Cursors.Default;
                toolStripStatusLabel1.Text = "Ready";


                if (((fileName1.Text).Equals("") && !checkBox15.Checked) && !(checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || checkBox8.Checked || checkBox9.Checked || checkBox10.Checked || checkBox11.Checked || checkBox12.Checked || checkBox13.Checked || checkBox14.Checked))
                {
                    label8.Text = "Enter File Name";
                    fileName1.BackColor = Color.Tomato;

                }
                else if ((!(fileName1.Text).Equals("") && !checkBox15.Checked) && !(checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || checkBox8.Checked || checkBox9.Checked || checkBox10.Checked || checkBox11.Checked || checkBox12.Checked || checkBox13.Checked || checkBox14.Checked ))
                {
                    label8.Text = "Please Select Any Extension";
                    fileName1.BackColor = Color.Tomato;
                }
         
                else
                {
                    label8.Text = "Please specify the Search Directory.";
                    directory1.BackColor = Color.Tomato;
                }
                //label4.Text = "Enter a Valid Filename";
               // fileName1.BackColor = Color.Tomato;

            
                
            }
            catch (DirectoryNotFoundException)
            {
                progressBar1.Value = 120;
                this.Cursor = Cursors.Default;
                toolStripStatusLabel1.Text = "Ready";
                //label4.Text = "Enter Valid Directory Path";

                label8.Text = "Enter Valid Directory Path";
                directory1.BackColor = Color.Tomato;
          
            }
            catch (IOException)
            {
                progressBar1.Value = 120;
                this.Cursor = Cursors.Default;
                toolStripStatusLabel1.Text = "Ready";
               
               
                
                //label4.Text = "Some Error,Please Check All The Fields";
                //label4.Text = "Enter a File name";
                
            }
            catch ( UnauthorizedAccessException )
            {
                progressBar1.Value = 120;
                this.Cursor = Cursors.Default;
                toolStripStatusLabel1.Text = "Ready";
                //label8.Text = "Can't Acess The Given Directory Path";
                int itemms = listView1.Items.Count;
                myitems = (itemms).ToString();
               // myString = (files.Length).ToString();
                if (!myString.Equals(""))
                    toolStripStatusLabel1.Text = "Files Scanned : " + myString + "   Files Retrieved : " + myitems;
                else
                    toolStripStatusLabel1.Text = "No Results";  
             
            }
            catch (FormatException)
            {
                progressBar1.Value = 120;
                this.Cursor = Cursors.Default;
                toolStripStatusLabel1.Text = "Ready";
               label8.Text = "Please Enter Valid Numeric Size";
               
               textBox1.BackColor = Color.Tomato;
               
            }
            catch(InvalidOperationException)
            {

            }

        }

        private void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            //throw new NotImplementedException();
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 1; i <= 100; i++)
            {
                // Wait 100 milliseconds.
                Thread.Sleep(10);
                backgroundWorker1.ReportProgress(i);


            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
               // progressBar1.Value = e.ProgressPercentage;
                 String textt = e.ProgressPercentage.ToString();
                 label7.Text = textt;

        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                String folderName = folderBrowserDialog1.SelectedPath;
                directory1.Text = folderName;
                
            }
        }
        
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
             String temp= openFileDialog1.FileName;
             String[] parts = temp.Split('\\');
           
             int k = parts.Length;
            int i;
            String tem = "";
            for (i = 0; i < k - 1; i++)
            {
                if (!tem.Equals(""))
                    tem = tem + "\\" + parts[i];
                else
                    tem = tem + parts[i];
            }
            directory1.Text = tem;
           
        }
        
        private void fileName1_TextChanged(object sender, EventArgs e)
        {
            fileName1.BackColor = Color.White;
            label8.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //openFileDialog2.ShowDialog();
            DialogResult result = folderBrowserDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                String folderName = folderBrowserDialog2.SelectedPath;
                textBox2.Text = folderName;

            }
            
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            String temp = openFileDialog2.FileName;
            String[] parts = temp.Split('\\');

            int k = parts.Length;
            int i;
            String tem = "";
            for (i = 0; i < k - 1; i++)
            {
                if (!tem.Equals(""))
                    tem = tem + "\\" + parts[i];
                else
                    tem = tem + parts[i];
            }
            textBox2.Text = tem;
           

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            label8.Text = "";
        }

        private void directory1_TextChanged(object sender, EventArgs e)
        {
            directory1.BackColor = Color.White;
            label8.Text = "";
        }


            
    }
      
}
