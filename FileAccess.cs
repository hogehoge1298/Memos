using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class FileAccess : MonoBehaviour {

	string _StrageDir = "";

    //public string fileName = "";

    //public SetVideoURL videoCtrl;

    //float timer = 0.0f;

    public Text pathText;

    //public ScrollController scrollCtrl;

	IEnumerator GetFilePath(){
#if !UNITY_EDITOR && UNITY_ANDROID
			using (AndroidJavaClass env = new AndroidJavaClass("android.os.Environment"))
			{
				using (AndroidJavaObject strageDir = env.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
				{
					_StrageDir = strageDir.Call<string>("getCanonicalPath");
					_StrageDir = _StrageDir + "/Download/360";
				}
			}			
#endif

        yield return null;
	}

    IEnumerator GetFilesFullPath(string dir, string extension, UnityEngine.Events.UnityAction<List<string>> callback)
    {
        List<string> stringList = new List<string>();

#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJavaObject file = new AndroidJavaObject("java.io.file", _StrageDir + dir);

        AndroidJavaObject[] files = file.Call<AndroidJavaObject[]>("listFiles");

        foreach(AndroidJavaObject obj in files)
        {
            if(obj.Call<bool>("isFile"))
            {
                string name = obj.Call<string>("getName");

                if(name.EndsWith(extension))
                {
                    stringList.Add(obj.Call<string>("getAbsolutePath"));
                }
            }
        }

#endif
        callback(stringList);
        yield return null;
    }

    public List<string> GetFilesFullPath(string extention, string dirName)
    {
        pathText.text = _StrageDir + "/" + dirName;
        DirectoryInfo dir = new DirectoryInfo(_StrageDir + dirName);
        FileInfo[] files = dir.GetFiles(extention);

        List<string> returnList = new List<string>();
        foreach(FileInfo f in files)
        {
            returnList.Add(f.FullName);
        }

        return returnList;
    }

    public List<string> SearchAllFiles(string extention, string dirName)
    {
        StartCoroutine(GetFilePath());

        List<string> returnList = new List<string>();
        //StartCoroutine(GetFilesFullPath(dirName, extention, (result) => returnList = result));
        returnList = new List<string>(GetFilesFullPath(extention, dirName));

        StopAllCoroutines();

        return returnList;
    }

    public bool IsExistFile(string path)
    {
        return File.Exists(path);
    }
}
