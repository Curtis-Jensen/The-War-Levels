using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void OpenBOMPage()
	{
#if !UNITY_EDITOR
		openWindow("https://www.churchofjesuschrist.org/study/scriptures/bofm/introduction?lang=eng");
#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}