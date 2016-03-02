
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Serialization;

public enum MessageTrigger {
	Default, Time, Event, Collider }

public struct Message {

	[YamlMember(Alias="trigger")]
	public MessageTrigger messageTrigger {get;set;}

	[YamlMember(Alias="title")]
	public string Title {get;set;}

	[YamlMember(Alias="desc")]
	public string Description {get;set;}

	[YamlMember(Alias="delay")]
	public float Delay {get;set;}

    public Message(
                    string title,
                    string description,
                    float delay,
                    MessageTrigger messageTrigger) : this() {
        this.Title = title;
        this.Description = description;
        this.Delay = delay;
        this.messageTrigger = messageTrigger;
    }

    public Message(string title) : this(title,"") { }

    public Message(string title, string description)
        : this(title,description,0f,MessageTrigger.Default) { }


}
