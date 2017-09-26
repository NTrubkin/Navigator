using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navigator
{
    public class Recorder : MonoBehaviour
    {
        private const string OUTPUT = "output.txt";

        private readonly List<string> tags = new List<string> { Gps.REC_X_TAG, Gps.REC_Z_TAG, AccelerometerAdapter.REC_X_TAG, AccelerometerAdapter.REC_Z_TAG, Odometer.REC_TAG };
        private readonly List<string[]> data = new List<string[]>();
        private string[] buffer;             // содержит линию данных

        public Recorder()
        {
            buffer = new string[tags.Count];
        }

        /// <summary>
        /// Запоминает одно значение в буфер
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tag"></param>
        public void SetData(string data, string tag)
        {
            if(tags.Contains(tag))
            {
                buffer[tags.IndexOf(tag)] = data;
                if (IsBufferFull())
                {
                    Commit();
                }
            }
            else
            {
                throw new System.ArgumentException("Tag " + tag + " does not exists");
            }
        }

        /// <summary>
        /// Фиксирует линию данных, очищает буфер
        /// </summary>
        private void Commit()
        {
            data.Add(buffer);
            buffer = new string[tags.Count];
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteToFile()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(OUTPUT);
            foreach (string[] line in data)
            {
                foreach (string element in line)
                {
                    file.Write(element + "\t");
                }
                file.WriteLine();
            }
            file.Close();
        }

        private bool IsBufferFull()
        {
            foreach (string element in buffer)
            {
                if (element == null)
                {
                    return false;
                }
            }
            return true;
        }

        public IList<string> GetTags()
        {
            return tags.AsReadOnly();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                WriteToFile();
            }
        }
    }

}