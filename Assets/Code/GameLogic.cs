using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameLogic : MonoBehaviour
{
	private readonly string[] criticRatings = new string[]
	{
		"D",
		"C-",
		"C",
		"C+",
		"B-",
		"B",
		"B+",
		"A-",
		"A",
		"A+"
	};

	[SerializeField]
	private Text subscribersText;
	[SerializeField]
	private Text moneyText;
	[SerializeField]
	private Text criticRatingText;

	[SerializeField]
	private Button titleButton;

	[SerializeField]
	private GameObject bookPanel;
	[SerializeField]
	private Text titleText;
	[SerializeField]
	private Text authorText;
	[SerializeField]
	private Text synopsisText;
	[SerializeField]
	private InputField reviewText;
	[SerializeField]
	private Image star1Image;
	[SerializeField]
	private Image star2Image;
	[SerializeField]
	private Image star3Image;
	[SerializeField]
	private Button submitButton;

	[SerializeField]
	private GameObject responsePanel;

	[SerializeField]
	private Text responseText;

	[SerializeField]
	private Text resultsText;



	private int currentStarRating;

	private Book currentBook;

	private int subcribers = 5;
	private int money = 100;
	private int criticRating = 3;


	private Data data;

	private List<int> bookChoices = new List<int>();



	public void OnTitleButton()
	{
		titleButton.gameObject.SetActive(false);
	}


    // Start is called before the first frame update
    private void Start()
    {
		SetUI();
		bookPanel.SetActive(true);
		responsePanel.SetActive(false);

		data = GetComponent<Data>();

		for(int bookIndex = 0; bookIndex < data.books.Length; ++ bookIndex)
		{
			bookChoices.Add(bookIndex);
		}
		
		SetBook();
	}

	private void SetUI()
	{
		subscribersText.text = subcribers.ToString();
		moneyText.text = "£" + money.ToString();
		criticRatingText.text = criticRatings[criticRating];
	}

	private void SetBook()
	{
		int randomIndex = Random.Range(0, bookChoices.Count);
		currentBook = data.books[bookChoices[randomIndex]];
		bookChoices.RemoveAt(randomIndex);

		titleText.text = currentBook.title;
		authorText.text = currentBook.author;
		synopsisText.text = currentBook.synopsis;

		currentStarRating = 1;
		star2Image.color = Color.black;
		star3Image.color = Color.black;

		EventSystem.current.SetSelectedGameObject(reviewText.gameObject, null);
	}

    // Update is called once per frame
    private void Update()
    {
        
    }

	public void OnStarRating(Button button)
	{
		switch(button.name)
		{
			case "Star1":
				currentStarRating = 1;
				star2Image.color = Color.black;
				star3Image.color = Color.black;
				break;

			case "Star2":
				currentStarRating = 2;
				star2Image.color = Color.white;
				star3Image.color = Color.black;
				break;

			case "Star3":
				currentStarRating = 3;
				star2Image.color = Color.white;
				star3Image.color = Color.white;
				break;
		}
	}

	public void OnSubmit()
	{
		bookPanel.SetActive(false);
		responsePanel.SetActive(true);

		Response currentResponse;

		switch (currentStarRating)
		{
			case 1:
				currentResponse = currentBook.star1Response;
				break;

			case 2:
				currentResponse = currentBook.star2Response;
				break;

			default:
			case 3:
				currentResponse = currentBook.star3Response;
				break;
		}
		
		responseText.text = currentResponse.text;

		string text = responseText.text;
		int replacementIndex = text.LastIndexOf("%");
		if(replacementIndex >= 0)
		{
			string[] sentences = reviewText.text.Split('.');
			if(sentences.Length > 0)
			{
				responseText.text = text.Replace("%", "\"" + sentences[Random.Range(0, sentences.Length)] + "\"");
			}
			else
			{
				responseText.text = text.Replace("%", "");
			}
		}
		else
		{
			responseText.text = text.Replace("%", "");
		}

		subcribers += currentResponse.fans;
		if(subcribers < 0)
		{
			subcribers = 0;
		}

		money += currentResponse.money;
		criticRating += currentResponse.prestige;
		if(criticRating <= 0)
		{
			criticRating = 0;
		}
		if (criticRating >= 10)
		{
			criticRating = 10;
		}

		SetUI();
	}

	public void OnNextReview()
	{
		//if(money <= 0 || subcribers <= 0)
		//{
		//	resultsText.text = "Your career as a critic has come to an abrupt end.";
		//	responsePanel.SetActive(false);
		//	return;
		//}

		if(bookChoices.Count <= 0)
		{
			responsePanel.SetActive(false);
			return;
		}


		bookPanel.SetActive(true);
		responsePanel.SetActive(false);
		reviewText.text = "";

		SetBook();

		switch (currentStarRating)
		{
			case 1:
				responseText.text = currentBook.star1Response.text;
				break;

			case 2:
				responseText.text = currentBook.star2Response.text;
				break;

			case 3:
				responseText.text = currentBook.star3Response.text;
				break;
		}
	}
}
