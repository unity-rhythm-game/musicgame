using UnityEngine;
using System.Collections;
using System.IO;

public class NotesTimingMaker : MonoBehaviour
{

	private AudioSource _audioSource;
	private float _startTime = 0;
	//private CSVWriter _CSVWriter;

	private bool _isPlaying = false;
	public GameObject startButton;
	public GameObject stopButton;

	public string filename;
	StreamWriter streamWriter;
	FileInfo fileInfo;

	void Start()
	{
		_audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource>(); //getcompornentで書き込み
		//_CSVWriter = GameObject.Find("CSVWriter").GetComponent<CSVWriter>();
		stopButton.SetActive(false);

	}

	void Update()
	{
		if (_isPlaying)// よく知らない
		{
			DetectKeys();
		}
	}

	public void StartMusic()
	{
		startButton.SetActive(false);
		stopButton.SetActive(true);
		_audioSource.Play();
		_startTime = Time.time;
		_isPlaying = true;


		
		fileInfo = new FileInfo(Application.dataPath + "/" + filename + ".csv");
		streamWriter = fileInfo.AppendText(); //謎多分これでファイル開いてる
	}

	public void StopMusic()
	{
		startButton.SetActive(true);
		stopButton.SetActive(false);//とりあえず反対のこと言ってみたらうまくいった
		_audioSource.Stop();
		_isPlaying = false;

		streamWriter.Flush(); //ここ
		streamWriter.Close();
	}

	void DetectKeys()
	{
		if (Input.GetKeyDown(KeyCode.D))
		{
			WriteNotesTiming(0);
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			WriteNotesTiming(1);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			WriteNotesTiming(2);
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			WriteNotesTiming(3);
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			WriteNotesTiming(4);
		}
	}

	void WriteNotesTiming(int num)
	{
		Debug.Log(GetTiming());

		streamWriter.WriteLine(GetTiming().ToString() + "," + num.ToString());//わざわざ別のゲームオブジェクトを開かないようにしたCSVWriterを消した
		//_CSVWriter.WriteCSV(GetTiming().ToString() + "," + num.ToString());
	}

	float GetTiming()
	{
		return Time.time - _startTime;
	}
}