Androidの内部ストレージのルートパスの取得方法  
  
```
void GetFilePath(){
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
	}
```
  
