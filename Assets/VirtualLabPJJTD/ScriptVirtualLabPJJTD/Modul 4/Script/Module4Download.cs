using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module4Download : MonoBehaviour
{
    private Module4SimManager simManager;
    private static extern void DownloadFile(string filename, string content);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        simManager = GetComponent<Module4SimManager>();
    }

    public string GenerateCSVFile()
    {
        string[] headers = { "No.", "Speed (km/hour)", "Dno (heart rate/minute)", "DNi (heart rate/minute)" };
        string[][] data = null;

        if(simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == false && simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == false)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "109" },
                new string[] {"1", "Pria", "15", "88", "116" },
                new string[] {"1", "Pria", "20", "88", "129" }
            };
            data = tempData;
        }
        if(simManager.isFemaleErgoDone == true && simManager.isMaleErgoDone == false && simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == false)
        {
            string[][] tempData =
            {
                new string[] {"1", "Wanita", "10", "80", "91" },
                new string[] {"1", "Wanita", "15", "80", "110" },
                new string[] {"1", "Wanita", "20", "80", "123" }
            };
            data = tempData;
        }
        if (simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == false && simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == false)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "109" },
                new string[] {"1", "Pria", "15", "88", "116" },
                new string[] {"1", "Pria", "20", "88", "129" }
            };
            data = tempData;
        }
        if (simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == false && simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == true)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "109" },
                new string[] {"1", "Pria", "15", "88", "116" },
                new string[] {"1", "Pria", "20", "88", "129" }
            };
            data = tempData;
        }

        if (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true && simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == false)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "84" },
                new string[] {"1", "Pria", "15", "88", "85" },
                new string[] {"1", "Pria", "20", "88", "113" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" }
            };
            data = tempData;
        }

        if (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == false && simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == false)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "84" },
                new string[] {"1", "Pria", "15", "88", "85" },
                new string[] {"1", "Pria", "20", "88", "113" },
                new string[] {"2", "Pria", "10", "80", "91" },
                new string[] {"2", "Pria", "15", "80", "110" },
                new string[] {"2", "Pria", "20", "80", "123" }
            };
            data = tempData;
        }
        if (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == false && simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == true)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "84" },
                new string[] {"1", "Pria", "15", "88", "85" },
                new string[] {"1", "Pria", "20", "88", "113" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" }
            };
            data = tempData;
        }
        if (simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == true && simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == false)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "84" },
                new string[] {"1", "Pria", "15", "88", "85" },
                new string[] {"1", "Pria", "20", "88", "113" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" }
            };
            data = tempData;
        }
        if (simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == true && simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == true)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "84" },
                new string[] {"1", "Pria", "15", "88", "85" },
                new string[] {"1", "Pria", "20", "88", "113" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" }
            };
            data = tempData;
        }
        if (simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == false && simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == true)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "84" },
                new string[] {"1", "Pria", "15", "88", "85" },
                new string[] {"1", "Pria", "20", "88", "113" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" }
            };
            data = tempData;
        }
        if (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true && simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == false)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "84" },
                new string[] {"1", "Pria", "15", "88", "85" },
                new string[] {"1", "Pria", "20", "88", "113" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" },
            };
            data = tempData;
        }
        if (simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == true && simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == true)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "84" },
                new string[] {"1", "Pria", "15", "88", "85" },
                new string[] {"1", "Pria", "20", "88", "113" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" },
            };
            data = tempData;
        }
        if (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true && simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == true)
        {
            string[][] tempData =
            {
                new string[] {"1", "Pria", "10", "88", "84" },
                new string[] {"1", "Pria", "15", "88", "85" },
                new string[] {"1", "Pria", "20", "88", "113" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" },
                new string[] {"2", "Wanita", "10", "80", "91" },
                new string[] {"2", "Wanita", "15", "80", "110" },
                new string[] {"2", "Wanita", "20", "80", "123" },
            };
            data = tempData;
        }

        System.Text.StringBuilder csv = new System.Text.StringBuilder();

        csv.AppendLine(string.Join(";", headers));

        foreach(var row in data)
        {
            csv.AppendLine(string.Join(";", row));
        }

        return csv.ToString();
    }

    public void ExportCSV()
    {
        try
        {
            string csv = GenerateCSVFile();
            DownloadFile("Data NIOSH.csv", csv);
        }
        catch
        {
            Debug.Log("Only run in webgl");
        }
    }

}
