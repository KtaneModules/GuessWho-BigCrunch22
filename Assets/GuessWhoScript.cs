using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class GuessWhoScript : MonoBehaviour
{
	public KMAudio Audio;
	public KMBombInfo Bomb;
	public KMBombModule Module;
	
	public AudioClip[] SFX;
	
	public KMSelectable FirstLeftButton;
	public KMSelectable FirstRightButton;
	public KMSelectable SecondLeftButton;
	public KMSelectable SecondRightButton;
	public KMSelectable ThirdLeftButton;
	public KMSelectable ThirdRightButton;
	public KMSelectable FourthLeftButton;
	public KMSelectable FourthRightButton;
	public KMSelectable FifthLeftButton;
	public KMSelectable FifthRightButton;
	
	public KMSelectable MainButton;
	
	public TextMesh FirstDisplay;
	public TextMesh SecondDisplay;
	public TextMesh ThirdDisplay;
	public TextMesh FourthDisplay;
	public TextMesh FifthDisplay;
	
	public TextMesh ButtonSays;
	
	public TextMesh PleaseWait;
	public TextMesh Dots;
	public TextMesh TheQuestion;
	public TextMesh TheAnswer;
	public TextMesh Exclaim;
	
	public TextMesh TheMarker;
	public TextMesh TheTrue;
	
	public Color TheOrange;
	public Color TheIndigo;
	public Color TheViolet;
	public Color ThePink;
	
	public string[] Names;
	
	private string[] Alphabet = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
	private int[] TheArray = {0, 1, 2, 3, 4, 5, 6, 7};
	private string[] DebugSequence = {"Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet", "Pink"};
	
	private int TheFirstInteger = 0;
	private int TheSecondInteger = 0;
	private int TheThirdInteger = 0;
	private int TheFourthInteger = 0;
	private int TheFifthInteger = 0;
	
	private int EighthPower = 0;
	private int SeventhPower = 0;
	private int SixthPower = 0;
	private int FifthPower = 0;
	private int FourthPower = 0;
	private int ThirdPower = 0;
	private int SecondPower = 0;
	private int FirstPower = 0;
	
	private int TheCombination = 0;
	private bool Playable = false; 
	
	private int Solvable = 0;
	
	private	int Order = 0;
	private int StackNumber = 0;
	
	private string Answer = "";
	
	//Logging
	static int moduleIdCounter = 1;
	int moduleId;
	private bool ModuleSolved;

	void Awake()
	{
		moduleId = moduleIdCounter++;
		FirstLeftButton.OnInteract += delegate() { PressFirstLeftButton(); return false; };
		FirstRightButton.OnInteract += delegate() { PressFirstRightButton(); return false; };
		SecondLeftButton.OnInteract += delegate() { PressSecondLeftButton(); return false; };
		SecondRightButton.OnInteract += delegate() { PressSecondRightButton(); return false; };
		ThirdLeftButton.OnInteract += delegate() { PressThirdLeftButton(); return false; };
		ThirdRightButton.OnInteract += delegate() { PressThirdRightButton(); return false; };
		FourthLeftButton.OnInteract += delegate() { PressFourthLeftButton(); return false; };
		FourthRightButton.OnInteract += delegate() { PressFourthRightButton(); return false; };
		FifthLeftButton.OnInteract += delegate() { PressFifthLeftButton(); return false; };
		FifthRightButton.OnInteract += delegate() { PressFifthRightButton(); return false; };
		MainButton.OnInteract += delegate() { PressMainButton(); return false; };
	}

	void Start()
	{
		Module.OnActivate += ActivateModule;
	}
	
	void ActivateModule()
	{
		Playable = true;
		AnswerOfTheDay();
		StateOfTheSequence();
		ButtonSays.text = "RECALL";
	}
	
	void StateOfTheSequence()
	{
		string FirstState = "";
		string SecondState = "";
		string ThirdState = "";
		string FourthState = "";
		string FifthState = "";
		string SixthState = "";
		string SeventhState = "";
		string EighthState = "";
		
		if (FirstPower == 1)
		{
			FirstState = "✓";
		}
		
		else if (FirstPower == 0)
		{
			FirstState = "✘";
		}
		
		if (SecondPower == 1)
		{
			SecondState = "✓";
		}
		
		else if (SecondPower == 0)
		{
			SecondState = "✘";
		}
		
		if (ThirdPower == 1)
		{
			ThirdState = "✓";
		}
		
		else if (ThirdPower == 0)
		{
			ThirdState = "✘";
		}
		
		if (FourthPower == 1)
		{
			FourthState = "✓";
		}
		
		else if (FourthPower == 0)
		{
			FourthState = "✘";
		}
		
		if (FifthPower == 1)
		{
			FifthState = "✓";
		}
		
		else if (FifthPower == 0)
		{
			FifthState = "✘";
		}
		
		if (SixthPower == 1)
		{
			SixthState = "✓";
		}
		
		else if (SixthPower == 0)
		{
			SixthState = "✘";
		}
		
		if (SeventhPower == 1)
		{
			SeventhState = "✓";
		}
		
		else if (SeventhPower == 0)
		{
			SeventhState = "✘";
		}
		
		if (EighthPower == 1)
		{
			EighthState = "✓";
		}
		
		else if (EighthPower == 0)
		{
			EighthState = "✘";
		}
		
		string[] Supper = {FirstState, SecondState, ThirdState, FourthState, FifthState, SixthState, SeventhState, EighthState};
		Debug.LogFormat("[Guess Who? #{0}] Sequence Given: {1}", moduleId, DebugSequence[TheArray[0]] + "(" + Supper[TheArray[0]] + ")" + ", " + DebugSequence[TheArray[1]] + "(" + Supper[TheArray[1]] + ")" + ", " + DebugSequence[TheArray[2]] + "(" + Supper[TheArray[2]] + ")" + ", " + DebugSequence[TheArray[3]] + "(" + Supper[TheArray[3]] + ")" + ", " + DebugSequence[TheArray[4]] + "(" + Supper[TheArray[4]] + ")" + ", " + DebugSequence[TheArray[5]] + "(" + Supper[TheArray[5]] + ")" + ", " + DebugSequence[TheArray[6]] + "(" + Supper[TheArray[6]] + ")" + ", " + DebugSequence[TheArray[7]] + "(" + Supper[TheArray[7]] + ")");

	}
	
	void AnswerOfTheDay()
	{
		TheArray.Shuffle();
		FirstPower = UnityEngine.Random.Range(0, 2);
		SecondPower = UnityEngine.Random.Range(0, 2);
		ThirdPower = UnityEngine.Random.Range(0, 2);
		FourthPower = UnityEngine.Random.Range(0, 2);
		FifthPower = UnityEngine.Random.Range(0, 2);
		SixthPower = UnityEngine.Random.Range(0, 2);
		SeventhPower = UnityEngine.Random.Range(0, 2);
		EighthPower = UnityEngine.Random.Range(0, 2);
		
		TheCombination = (FirstPower * 128) + (SecondPower * 64) + (ThirdPower * 32) + (FourthPower * 16) + (FifthPower * 8) + (SixthPower * 4) + (SeventhPower * 2) + (EighthPower * 1);
		
		Answer = Names[TheCombination].ToUpper();
		Debug.LogFormat("[Guess Who? #{0}] Number Gathered: {1}", moduleId, TheCombination.ToString());
		Debug.LogFormat("[Guess Who? #{0}] Name of the Person: {1} ", moduleId, Answer);
	}
	
	void TheScreens()
	{
		FirstDisplay.text = Alphabet[TheFirstInteger];
		SecondDisplay.text = Alphabet[TheSecondInteger];
		ThirdDisplay.text = Alphabet[TheThirdInteger];
		FourthDisplay.text = Alphabet[TheFourthInteger];
		FifthDisplay.text = Alphabet[TheFifthInteger];
	}
	
	void PressFirstLeftButton()
	{
		FirstLeftButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable == 1)
		{
		TheFirstInteger = ((TheFirstInteger - 1) + 26) % 26;
		TheScreens();
		}
	}
	
	void PressFirstRightButton()
	{
		FirstRightButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable == 1)
		{
		TheFirstInteger = (TheFirstInteger + 1) % 26;
		TheScreens();
		}
	}
	
	void PressSecondLeftButton()
	{
		SecondLeftButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable == 1)
		{
		TheSecondInteger = ((TheSecondInteger - 1) + 26) % 26;
		TheScreens();
		}
	}
	
	void PressSecondRightButton()
	{
		SecondRightButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable == 1)
		{
		TheSecondInteger = (TheSecondInteger + 1) % 26;
		TheScreens();
		}
	}
	
	void PressThirdLeftButton()
	{
		ThirdLeftButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable == 1)
		{
		TheThirdInteger = ((TheThirdInteger - 1) + 26) % 26;
		TheScreens();
		}
	}
	
	void PressThirdRightButton()
	{
		ThirdRightButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable == 1)
		{
		TheThirdInteger = (TheThirdInteger + 1) % 26;
		TheScreens();
		}
	}
	
	void PressFourthLeftButton()
	{
		FourthLeftButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable == 1)
		{
		TheFourthInteger = ((TheFourthInteger - 1) + 26) % 26;
		TheScreens();
		}
	}
	
	void PressFourthRightButton()
	{
		FourthRightButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable == 1)
		{
		TheFourthInteger = (TheFourthInteger + 1) % 26;
		TheScreens();
		}
	}
	
	void PressFifthLeftButton()
	{
		FifthLeftButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable == 1)
		{
		TheFifthInteger = ((TheFifthInteger - 1) + 26) % 26;
		TheScreens();
		}
	}
	
	void PressFifthRightButton()
	{
		FifthRightButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable == 1)
		{
		TheFifthInteger = (TheFifthInteger + 1) % 26;
		TheScreens();
		}
	}
	
	void PressMainButton()
	{
		MainButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[2].name, transform);
		if (Solvable == 0 && Playable == true)
		{
			StartCoroutine(ProcessingTheImages());
			Solvable = 2;
		}
		
		else if (Solvable == 1)
		{
		Debug.LogFormat("[Guess Who? #{0}] You Submitted: {1}", moduleId, Alphabet[TheFirstInteger] + Alphabet[TheSecondInteger] + Alphabet[TheThirdInteger] + Alphabet[TheFourthInteger] + Alphabet[TheFifthInteger]);
			if ((Alphabet[TheFirstInteger] + Alphabet[TheSecondInteger] + Alphabet[TheThirdInteger] + Alphabet[TheFourthInteger] + Alphabet[TheFifthInteger]) == Answer)
			{
				Solvable = 3;
				TheMarker.text = "";
				ButtonSays.text = "";
				StartCoroutine(YouGotIt());
			}
			else
			{
				Solvable = 3;
				TheMarker.text = "";
				ButtonSays.text = "";
				StartCoroutine(ThatIsNotIt());
			}
		}
	}	

	IEnumerator ProcessingTheImages()
	{
		ButtonSays.text = "";
		string Waiting = "Please\nWait";
		for (int j = 0; j < Waiting.Length; j++)
		{
			PleaseWait.text += Waiting[j].ToString();
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(0.625f);
		string Dotes = "........";
		for (int v = 0; v < Dotes.Length; v++)
		{
			Dots.text += Dotes[v].ToString();
			Audio.PlaySoundAtTransform(SFX[0].name, transform);
			yield return new WaitForSeconds(0.625f);
		}
		StartCoroutine(TheFollowing());
		
	}
	
	IEnumerator TheFollowing()
	{
		PleaseWait.text = "";
		Dots.text = "";
		string Delayed = "Is the\nstatement\ntrue";
		for (int x = 0; x < Delayed.Length; x++)
		{
			yield return new WaitForSeconds(0.0425f);
			TheQuestion.text += Delayed[x].ToString();
		}
		StartCoroutine(TheProcess());
	}
	
	void Coloring()
	{
		if (TheArray[Order] == 0)
		{
			TheAnswer.color = Color.red;
		}
		
		else if (TheArray[Order] == 1)
		{
			TheAnswer.color = TheOrange;
		}
		
		else if (TheArray[Order] == 2)
		{
			TheAnswer.color = Color.yellow;
		}
		
		else if (TheArray[Order] == 3)
		{
			TheAnswer.color = Color.green;
		}
		
		else if (TheArray[Order] == 4)
		{
			TheAnswer.color = Color.blue;
		}
		
		else if (TheArray[Order] == 5)
		{
			TheAnswer.color = TheIndigo;
		}
		
		else if (TheArray[Order] == 6)
		{
			TheAnswer.color = TheViolet;
		}
		
		else if (TheArray[Order] == 7)
		{
			TheAnswer.color = ThePink;
		}
	}
	
	IEnumerator TheProcess()
	{
		int ForestTree = 0;
		while(ForestTree < 8)
		{
			Coloring();
			if (TheArray[Order] == 0)
			{
				if (FirstPower == 0)
				{
					yield return new WaitForSeconds(0.2f);	
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "NO";
					yield return new WaitForSeconds(0.4f);
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "";
				}
				
				else if (FirstPower == 1)
				{
					yield return new WaitForSeconds(0.14f);	
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YES";
					yield return new WaitForSeconds(0.3f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "";
				}
			}	
			
			else if (TheArray[Order] == 1)
			{
				if (SecondPower == 0)
				{
					yield return new WaitForSeconds(0.2f);	
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "NO";
					yield return new WaitForSeconds(0.4f);
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "";
				}
				
				else if (SecondPower == 1)
				{
					yield return new WaitForSeconds(0.14f);	
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YES";
					yield return new WaitForSeconds(0.3f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "";
				}
			}
			
			else if (TheArray[Order] == 2)
			{
					
				if (ThirdPower == 0)
				{
					yield return new WaitForSeconds(0.2f);	
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "NO";
					yield return new WaitForSeconds(0.4f);
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "";
				}
				
				else if (ThirdPower == 1)
				{
					yield return new WaitForSeconds(0.14f);	
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YES";
					yield return new WaitForSeconds(0.3f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "";
				}
			}
				
			else if (TheArray[Order] == 3)
			{
				if (FourthPower == 0)
				{
					yield return new WaitForSeconds(0.2f);	
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "NO";
					yield return new WaitForSeconds(0.4f);
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "";
				}
				
				else if (FourthPower == 1)
				{
					yield return new WaitForSeconds(0.14f);	
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YES";
					yield return new WaitForSeconds(0.3f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "";
				}
			}	
			
			else if (TheArray[Order] == 4)
			{
				if (FifthPower == 0)
				{
					yield return new WaitForSeconds(0.2f);	
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "NO";
					yield return new WaitForSeconds(0.4f);
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "";
				}
				
				else if (FifthPower == 1)
				{
					yield return new WaitForSeconds(0.14f);	
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YES";
					yield return new WaitForSeconds(0.3f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "";
				}
			}	
			
			else if (TheArray[Order] == 5)
			{
				if (SixthPower == 0)
				{
					yield return new WaitForSeconds(0.2f);	
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "NO";
					yield return new WaitForSeconds(0.4f);
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "";
				}
				
				else if (SixthPower == 1)
				{
					yield return new WaitForSeconds(0.14f);	
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YES";
					yield return new WaitForSeconds(0.3f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "";
				}
			}
			
			else if (TheArray[Order] == 6)
			{
				if (SeventhPower == 0)
				{
					yield return new WaitForSeconds(0.2f);	
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "NO";
					yield return new WaitForSeconds(0.4f);
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "";
				}
				
				else if (SeventhPower == 1)
				{
					yield return new WaitForSeconds(0.14f);	
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YES";
					yield return new WaitForSeconds(0.3f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "";
				}
			}

			else if (TheArray[Order] == 7)
			{
				if (EighthPower == 0)
				{
					yield return new WaitForSeconds(0.2f);	
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "NO";
					yield return new WaitForSeconds(0.4f);
					TheAnswer.text = "N";
					yield return new WaitForSeconds(0.2f);
					TheAnswer.text = "";
				}
				
				else if (EighthPower == 1)
				{
					yield return new WaitForSeconds(0.14f);	
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "YES";
					yield return new WaitForSeconds(0.3f);
					TheAnswer.text = "YE";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "Y";
					yield return new WaitForSeconds(0.14f);
					TheAnswer.text = "";
				}
			}
			yield return new WaitForSeconds(0.5f);
			ForestTree++;
			Order++;
		}
		
		if (Order == 8)
		{
			StartCoroutine(WhoAmI());
		}
	}
	
	IEnumerator WhoAmI()
	{
		TheQuestion.text = "";
		TheMarker.text = "?";
		Audio.PlaySoundAtTransform(SFX[4].name, transform);
		yield return new WaitForSeconds(3f);
		FirstDisplay.text = Alphabet[TheFirstInteger];
		Audio.PlaySoundAtTransform(SFX[3].name, transform);
		yield return new WaitForSeconds(0.2f);
		SecondDisplay.text = Alphabet[TheSecondInteger];
		Audio.PlaySoundAtTransform(SFX[3].name, transform);
		yield return new WaitForSeconds(0.2f);
		ThirdDisplay.text = Alphabet[TheThirdInteger];
		Audio.PlaySoundAtTransform(SFX[3].name, transform);
		yield return new WaitForSeconds(0.2f);
		FourthDisplay.text = Alphabet[TheFourthInteger];
		Audio.PlaySoundAtTransform(SFX[3].name, transform);
		yield return new WaitForSeconds(0.2f);
		FifthDisplay.text = Alphabet[TheFifthInteger];
		Audio.PlaySoundAtTransform(SFX[3].name, transform);
		yield return new WaitForSeconds(0.2f);
		ButtonSays.text = "G";
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		yield return new WaitForSeconds(0.2f);
		ButtonSays.text = "GU";
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		yield return new WaitForSeconds(0.2f);
		ButtonSays.text = "GUE";
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		yield return new WaitForSeconds(0.2f);
		ButtonSays.text = "GUES";
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		yield return new WaitForSeconds(0.2f);
		ButtonSays.text = "GUESS";
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		Solvable = 1;
	}
	
	IEnumerator NotCopycat()
	{
		if (StackNumber == 248)
		{
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "N";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "No";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "Not\n";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "Not\nR";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "Not\nRi";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "Not\nRig";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "Not\nRigh";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "Not\nRight";
		}	
		
		if (StackNumber == 300)
		{
			FirstDisplay.color = Color.red;
			SecondDisplay.color = Color.red;
			ThirdDisplay.color = Color.red;
			FourthDisplay.color = Color.red;
			FifthDisplay.color = Color.red;
			FirstDisplay.text = "R";
			SecondDisplay.text = "E";
			ThirdDisplay.text = "S";
			FourthDisplay.text = "E";
			FifthDisplay.text = "T";
			Debug.LogFormat("[Guess Who? #{0}] Name does not match. Try again!", moduleId);
			yield return new WaitForSeconds(1f);
			Module.HandleStrike();
			Reset();
			ActivateModule();
			
		}	
	}
	
	IEnumerator Copycat()
	{
		if (StackNumber == 248)
		{
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "Y";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "Yo";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "You\n";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "You\nG";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "You\nGo";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "You\nGot\n";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "You\nGot I";
			yield return new WaitForSeconds(0.1f);
			TheTrue.text = "You\nGot It";
		}	
		
		if (StackNumber == 300)
		{
			FirstDisplay.color = Color.green;
			SecondDisplay.color = Color.green;
			ThirdDisplay.color = Color.green;
			FourthDisplay.color = Color.green;
			Exclaim.color = Color.green;
			FirstDisplay.text = "N";
			SecondDisplay.text = "I";
			ThirdDisplay.text = "C";
			FourthDisplay.text = "E";
			FifthDisplay.text = "";
			Exclaim.text = "!";			
			Debug.LogFormat("[Guess Who? #{0}] Its a match. Module is done!", moduleId);
			Audio.PlaySoundAtTransform(SFX[5].name, transform);
			Module.HandlePass();
		}	
	}
	
	
	IEnumerator YouGotIt()
	{
		while (StackNumber < 300)
		{
			int rand1 = UnityEngine.Random.Range(0, 26);
			int rand2 = UnityEngine.Random.Range(0, 26);
			int rand3 = UnityEngine.Random.Range(0, 26);
			int rand4 = UnityEngine.Random.Range(0, 26);
			int rand5 = UnityEngine.Random.Range(0, 26);
			FirstDisplay.text = Alphabet[rand1];
			SecondDisplay.text = Alphabet[rand2];
			ThirdDisplay.text = Alphabet[rand3];
			FourthDisplay.text = Alphabet[rand4];
			FifthDisplay.text = Alphabet[rand5];
			StackNumber++;
			StartCoroutine(Copycat());
			yield return new WaitForSeconds(0.01f);
		}
	}
	
	IEnumerator ThatIsNotIt()
	{
		while (StackNumber < 300)
		{
			int rand1 = UnityEngine.Random.Range(0, 26);
			int rand2 = UnityEngine.Random.Range(0, 26);
			int rand3 = UnityEngine.Random.Range(0, 26);
			int rand4 = UnityEngine.Random.Range(0, 26);
			int rand5 = UnityEngine.Random.Range(0, 26);
			FirstDisplay.text = Alphabet[rand1];
			SecondDisplay.text = Alphabet[rand2];
			ThirdDisplay.text = Alphabet[rand3];
			FourthDisplay.text = Alphabet[rand4];
			FifthDisplay.text = Alphabet[rand5];
			StackNumber++;
			StartCoroutine(NotCopycat());
			yield return new WaitForSeconds(0.01f);
		}
	}
	
	void Reset()
	{
	TheFirstInteger = 0;
	TheSecondInteger = 0;
	TheThirdInteger = 0;
	TheFourthInteger = 0;
	TheFifthInteger = 0;
	
	TheCombination = 0;
	
	Solvable = 0;
	
	Order = 0;
	StackNumber = 0;
	
	FirstDisplay.text = "";
	SecondDisplay.text = "";
	ThirdDisplay.text = "";
	FourthDisplay.text = "";
	FifthDisplay.text = "";
	TheTrue.text = "";
	
	FirstDisplay.color = Color.white;
	SecondDisplay.color = Color.white;
	ThirdDisplay.color = Color.white;
	FourthDisplay.color = Color.white;
	FifthDisplay.color = Color.white;
	}
}
