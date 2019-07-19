using UnityEngine;

[System.Serializable]
public class Book
{
	[SerializeField]
	public string title;

	[SerializeField]
	public string author;

	[SerializeField]
	public string synopsis;

	[SerializeField]
	public Response star1Response;

	[SerializeField]
	public Response star2Response;

	[SerializeField]
	public Response star3Response;
}
