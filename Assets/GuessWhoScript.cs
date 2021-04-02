using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class GuessWhoScript : MonoBehaviour
{
	public KMAudio Audio;
	public KMBombInfo Bomb;
	public KMBombModule Module;
	
	public AudioClip[] SFX;
	public KMSelectable[] ButtonLeft;
	public KMSelectable[] ButtonRight;
	public KMSelectable MainButton;
	
	public TextMesh[] Displays;
	public TextMesh ButtonSays;
	
	public TextMesh PleaseWait;
	public TextMesh Dots;
	public TextMesh TheQuestion;
	public TextMesh TheAnswer;
	public TextMesh Exclaim;
	
	public TextMesh TheMarker;
	public TextMesh TheTrue;
	
	public Color[] ROYGBIVP;
	public string[] Names;
	string[] Alphabet = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
	string[] DebugSequence = {"Red", "Orange", "Yellow", "Green", "Blue", "Violet", "Cyan", "Pink"};
	int[] TheArray = {0, 1, 2, 3, 4, 5, 6, 7};
	
	int[] NumberBase = {0,0,0,0,0};
	int[] Bases = new int[8];
	int TheCombination = 0;
	bool Playable = true; 
	bool Solvable = false;
	string Baseline;
	Coroutine CoroSpin;
	
	//Logging
	static int moduleIdCounter = 1;
	int moduleId;
	private bool ModuleSolved;
	
	void Awake()
    {
        moduleId = moduleIdCounter++;
        for (int a = 0; a < 5; a++)
        {
			int LeftPress = a;
            ButtonLeft[a].OnInteract += delegate
            {
                Left(LeftPress);
                return false;
            };
        }
		
		for (int b = 0; b < 5; b++)
        {
			int RightPress = b;
            ButtonRight[b].OnInteract += delegate
            {
                Right(RightPress);
                return false;
            };
        }
		
		MainButton.OnInteract += delegate() { MainFunction(); return false; };
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
	
	void Left(int LeftPress)
	{
		ButtonLeft[LeftPress].AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable && Playable)
		{
			NumberBase[LeftPress] = ((NumberBase[LeftPress] - 1) + 26) % 26;
			Displays[LeftPress].text = Alphabet[NumberBase[LeftPress]];
		}
	}
	
	void Right(int RightPress)
	{
		ButtonRight[RightPress].AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[6].name, transform);
		if (Solvable && Playable)
		{
			NumberBase[RightPress] = (NumberBase[RightPress] + 1) % 26;
			Displays[RightPress].text = Alphabet[NumberBase[RightPress]];
		}
	}
	
	void MainFunction()
	{
		MainButton.AddInteractionPunch(0.2f);
		Audio.PlaySoundAtTransform(SFX[2].name, transform);
		if (Playable && !Solvable)
		{
			StartCoroutine(ProcessingTheNames());
			Playable = false;
		}
		
		else if(Playable && Solvable)
		{
			Playable = false;
			Solvable = true;
			StartCoroutine(ProcessingTheInput());
		}
	}
	
	void AnswerOfTheDay()
	{
		TheArray.Shuffle();
		for (int x = 0; x < 8; x++)
		{
			Bases[x] = UnityEngine.Random.Range(0,2);
			if (Bases[x] == 1)
			{
				int Add = 1;
				for (int y = 0; y < 7 - x; y++)
				{
					Add *= 2;
				}
				TheCombination += Add;
			}
		}
		Debug.LogFormat("[Guess Who? #{0}] Number Gathered: {1}", moduleId, TheCombination.ToString());
		Debug.LogFormat("[Guess Who? #{0}] Name of the Person: {1} ", moduleId, Names[TheCombination]);
	}
	
	void StateOfTheSequence()
	{
		string[] States = new string[8];
		string Throwaway = "";
		for (int x = 0; x < 8; x++)
		{
			if (Bases[TheArray[x]] == 1)
			{
				States[TheArray[x]] = "✓";
			}
			
			else
			{
				States[TheArray[x]] = "✘";
			}
			
			Throwaway += DebugSequence[TheArray[x]] + "(" + States[TheArray[x]] + ")";
			if (x != 7) Throwaway += ", ";
		}
		
		Debug.LogFormat("[Guess Who? #{0}] Sequence Given: {1}", moduleId, Throwaway);
	}
	
	IEnumerator ProcessingTheNames()
	{
		Recalling = true;
		ButtonSays.text = "";
		string Waiting = "Please\nWait";
		for (int j = 0; j < Waiting.Length; j++)
		{
			PleaseWait.text += Waiting[j].ToString();
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(0.625f);
		Waiting = "........";
		for (int v = 0; v < Waiting.Length; v++)
		{
			Dots.text += Waiting[v].ToString();
			Audio.PlaySoundAtTransform(SFX[0].name, transform);
			yield return new WaitForSeconds(0.625f);
		}
		StartCoroutine(Preparation());
	}
	
	IEnumerator Preparation()
	{
		PleaseWait.text = "";
		Dots.text = "";
		string Delayed = "Is the\nstatement\ntrue";
		for (int x = 0; x < Delayed.Length; x++)
		{
			yield return new WaitForSeconds(0.0425f);
			TheQuestion.text += Delayed[x].ToString();
		}
		StartCoroutine(TheCycle());
	}
	
	IEnumerator TheCycle()
	{
		for (int x = 0; x < 8; x++)
		{
			TheAnswer.color = ROYGBIVP[TheArray[x]];
			if (Bases[TheArray[x]] == 1)
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
			
			else
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
			yield return new WaitForSeconds(0.5f);
		}
		StartCoroutine(WhoAmI());
	}
	
	IEnumerator WhoAmI()
	{
		TheQuestion.text = "";
		TheMarker.text = "?";
		Audio.PlaySoundAtTransform(SFX[4].name, transform);
		yield return new WaitForSeconds(3f);
		for (int x = 0; x < 5; x++)
		{
			Displays[x].text = Alphabet[NumberBase[x]];
			Audio.PlaySoundAtTransform(SFX[3].name, transform);
			yield return new WaitForSeconds(0.2f);
		}
		string Guess = "GUESS";
		for (int y = 0; y < 5; y++)
		{
			ButtonSays.text += Guess[y].ToString();
			Audio.PlaySoundAtTransform(SFX[1].name, transform);
			yield return new WaitForSeconds(0.2f);
		}
		Solvable = true;
		Playable = true;
		Recalling = false;
		Guessing = true;
	}
	
	IEnumerator CycleButton()
	{
		while (true)
		{
			for (int a = 0; a < 5; a++)
			{
				Displays[a].text = Alphabet[UnityEngine.Random.Range(0,26)];
			}
			yield return new WaitForSeconds(0.01f);
		}
	}
	
	IEnumerator ProcessingTheInput()
	{
		Processing = true;
		TheMarker.text = "";
		ButtonSays.text = "";
		for (int b = 0; b < 5; b++)
		{
			Baseline += Displays[b].text;
		}
		Debug.LogFormat("[Guess Who? #{0}] You submitted: {1}", moduleId, Baseline);
		CoroSpin = StartCoroutine(CycleButton());
		yield return new WaitForSecondsRealtime(3f);
		StartCoroutine(ResultingDisplay());
	}
	
	IEnumerator ResultingDisplay()
	{
		Playable = false;
		string[] Results = {"Not\nRight", "You\nGot It", "NICE ", "RESET"};
		if (Baseline == Names[TheCombination])
		{
			for (int x = 0; x < Results[1].Length; x++)
			{
				TheTrue.text += Results[1][x].ToString();
				yield return new WaitForSeconds(0.1f);
			}
			StopCoroutine(CoroSpin);
			for (int y = 0; y < 5; y++)
			{
				Displays[y].text = "";
				Displays[y].color = Color.green;
				Displays[y].text = Results[2][y].ToString();
			}
			Exclaim.text = "!";	
			Exclaim.color = Color.green;
			Debug.LogFormat("[Guess Who? #{0}] It's a match. Module is done!", moduleId);
			Audio.PlaySoundAtTransform(SFX[5].name, transform);
			Module.HandlePass();
			Processing = false;
		}
		
		else
		{
			for (int x = 0; x < Results[0].Length; x++)
			{
				TheTrue.text += Results[0][x].ToString();
				yield return new WaitForSeconds(0.1f);
			}
			StopCoroutine(CoroSpin);
			for (int y = 0; y < 5; y++)
			{
				Displays[y].color = Color.red;
				Displays[y].text = Results[3][y].ToString();

			}
			Debug.LogFormat("[Guess Who? #{0}] The name does not match. Try again!", moduleId);
			yield return new WaitForSeconds(1f);
			Module.HandleStrike();
			for (int z = 0; z < 5; z++)
			{
				Displays[z].color = Color.white;
				Displays[z].text = "";
				NumberBase[z] = 0;
			}
			TheTrue.text = "";
			TheCombination = 0;
			ActivateModule();
			Baseline = "";
			Playable = true;
			Solvable = false;
			Guessing = false;
			Processing = false;
		}
	}
	
	//twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"To start the module, use !{0} recall, or !{0} recallfocus to recall the answers | To submit the name in the module, use !{0} guess [NAME] | The name must be typed in all capital letters.";
    #pragma warning restore 414
	
	bool Recalling = false;
	bool Guessing = false;
	bool Processing = false;
	
	IEnumerator ProcessTwitchCommand(string command)
	{
		string[] parameters = command.Split(' ');
		if (Recalling)
		{
			yield return "sendtochaterror The module is recalling. The command was not processed.";
			yield break;
		}
		
		if (Processing)
		{
			yield return "sendtochaterror The module is currently processing the answer. The command was not processed.";
			yield break;
		}
			
		if (Regex.IsMatch(command, @"^\s*recall\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
		{
			yield return null;
			
			if (Guessing)
			{
				yield return "sendtochaterror You are currently guessing a person. The command was not processed.";
				yield break;
			}
			MainButton.OnInteract();
		}
		
		if (Regex.IsMatch(command, @"^\s*recallfocus\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
		{
			yield return null;
			if (Guessing)
			{
				yield return "sendtochaterror You are currently guessing a person. The command was not processed.";
				yield break;
			}
			
			MainButton.OnInteract();
			while (Recalling)
			{
				yield return null;
			}
		}
		
		if (Regex.IsMatch(parameters[0], @"^\s*guess\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
		{
			yield return null;
			if (parameters.Length > 2 || parameters.Length < 2)
			{
				yield return "sendtochaterror Parameter length is not valid. The command was not processed.";
				yield break;
			}
			
			if (!Guessing)
			{
				yield return "sendtochaterror You are currently not guessing a person. The command was not processed.";
				yield break;
			}
			
			if (parameters[1].Length > 5 || parameters[1].Length < 5)
			{
				yield return "sendtochaterror Name length is too long/short. The command was not processed.";
				yield break;
			}
			
			foreach (char c in parameters[1])
			{
				if (!c.ToString().EqualsAny(Alphabet))
				{
					yield return "sendtochaterror The name contains an invalid character. The command was not processed.";
					yield break;
				}
			}

			for (int x = 0; x < parameters[1].Length; x++)
			{
				int ct1 = 0;
				int ct2 = 0;
				int start = NumberBase[x];
				int check = Array.IndexOf(Alphabet, parameters[1][x].ToString());
				while (start != check)
                {
					start++;
					ct1++;
					if (start > 25)
						start = 0;
                }
				start = NumberBase[x];
				while (start != check)
				{
					start--;
					ct2++;
					if (start < 0)
						start = 25;
				}
				if (ct1 < ct2)
                {
					for (int i = 0; i < ct1; i++)
                    {
						ButtonRight[x].OnInteract();
						yield return new WaitForSecondsRealtime(0.05f);
					}
                }
				else if (ct2 < ct1)
                {
					for (int i = 0; i < ct2; i++)
					{
						ButtonLeft[x].OnInteract();
						yield return new WaitForSecondsRealtime(0.05f);
					}
				}
                else
                {
					int type = UnityEngine.Random.Range(0, 2);
					if (type == 0)
                    {
						for (int i = 0; i < ct1; i++)
						{
							ButtonLeft[x].OnInteract();
							yield return new WaitForSecondsRealtime(0.05f);
						}
					}
                    else
                    {
						for (int i = 0; i < ct1; i++)
						{
							ButtonRight[x].OnInteract();
							yield return new WaitForSecondsRealtime(0.05f);
						}
					}
                }
			}
			yield return "solve";
			yield return "strike";
			MainButton.OnInteract();
		}
	}

	IEnumerator TwitchHandleForcedSolve()
    {
		if (Processing && Baseline != Names[TheCombination])
        {
			StopAllCoroutines();
			string[] Results = { "You\nGot It", "NICE " };
			TheTrue.text = "";
			for (int x = 0; x < Results[0].Length; x++)
				TheTrue.text += Results[0][x].ToString();
			for (int y = 0; y < 5; y++)
			{
				Displays[y].text = "";
				Displays[y].color = Color.green;
				Displays[y].text = Results[1][y].ToString();
			}
			Exclaim.text = "!";
			Exclaim.color = Color.green;
			Audio.PlaySoundAtTransform(SFX[5].name, transform);
			Module.HandlePass();
			yield break;
		}
		if (!Processing)
        {
			if (!Recalling && !Guessing)
				MainButton.OnInteract();
			while (Recalling) yield return true;
			yield return ProcessTwitchCommand("guess " + Names[TheCombination]);
		}
		while (Processing) yield return true;
	}
}