using Common.Data;
using Managers;
using Models;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class NpcController : MonoBehaviour
{
    public int npcID;
    public GameObject UINpcNameBar;

    private Animator animator;
    private Image UINpcNameBarBG;
    private Color orignColor;
    private bool inInteractive = false;

    private NpcDefine npc;

    private void Start()
    {
        UINpcNameBarBG = UINpcNameBar.GetComponent<Image>();
        orignColor = UINpcNameBarBG.color;
        animator = this.gameObject.GetComponent<Animator>();
        npc = NpcManager.Instance.GetNpcDefine(npcID);
        this.StartCoroutine(Actions());
    }

    private IEnumerator Actions()
    {
        while (true)
        {
            if (inInteractive)
                yield return new WaitForSeconds(2f);
            else
            {
                yield return new WaitForSeconds(Random.Range(5f, 10f));
            }
            this.Relax();
        }
    }

    private void Relax()
    {
        animator.SetTrigger("Relax");
    }
    private void Interactive()
    {
        if (!inInteractive)
        {
            inInteractive = true;
            StartCoroutine(DoInInteractive());
        }
    }

    private IEnumerator DoInInteractive()
    {
        yield return FaceToPlayer();
        if (NpcManager.Instance.Interactive(npc))
        {
            animator.SetTrigger("Talk");
        }
        yield return new WaitForSeconds(3f);
        inInteractive = false;
    }

    private IEnumerator FaceToPlayer()
    {
        Vector3 factTo = (User.Instance.CurrentCharacterObject.transform.position - this.transform.position).normalized;
        while (Mathf.Abs(Vector3.Angle(this.gameObject.transform.forward, factTo)) > 5)
        {
            this.gameObject.transform.forward = Vector3.Lerp(this.gameObject.transform.forward, factTo, Time.deltaTime * 5f);
            yield return null;
        }
    }

    private void OnMouseDown()
    {
        Interactive();
    }

    private void OnMouseOver()
    {
        Highlight(true);
    }

    private void OnMouseEnter()
    {
        Highlight(true);
    }

    private void OnMouseExit()
    {
        Highlight(false);
    }

    private void Highlight(bool highlight)
    {
        if (highlight)
        {
            if (UINpcNameBarBG.color != Color.yellow)
                UINpcNameBarBG.color = Color.yellow;
        }
        else
        {
            if (UINpcNameBarBG.color != orignColor)
                UINpcNameBarBG.color = orignColor;
        }
    }
}