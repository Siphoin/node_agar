using System;
[Serializable]
 public   class Message
    {
    public string id = "";
  public  string dataMessage = "";

    public Message()
    {

    }

    public Message(string text, string ID)
    {
        dataMessage = text;
        id = ID;
    }
    }
