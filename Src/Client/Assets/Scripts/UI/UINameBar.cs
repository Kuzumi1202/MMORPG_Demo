using Entities;
using UnityEngine;
using UnityEngine.UI;

public class UINameBar : MonoBehaviour
{
    public Text avaverName;
    public Character character;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        this.UpdateInfo();
        this.transform.forward = Camera.main.transform.forward;
    }

    private void UpdateInfo()
    {
        if (this.character != null)
        {
            string name = this.character.Name + " [Lv" + this.character.Info.Level + "]";
            if (name != this.avaverName.text)
            {
                this.avaverName.text = name;
            }
        }
    }
}