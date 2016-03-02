using UnityEngine;
using ui=UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Narration : MonoBehaviour {

	string initMessageName = "init";

	ui::Text text;
	public Message message;


	void Awake() {
		text = GetComponent<ui::Text>()
			?? GetComponentInChildren<ui::Text>();
		if (!text)
			throw new System.Exception("No Text");
		DisplayMessage(initMessageName);
	}

	public void DisplayMessage() {
		DisplayMessage(message); }

	public void DisplayMessage(string name) {
		DisplayMessage(yml.messages[name]); }

	public void DisplayMessage(Message message) {
		text.text = message.Description.md();
	}
}
