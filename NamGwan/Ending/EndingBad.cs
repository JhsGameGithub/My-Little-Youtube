using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public class EndingBad : MonoBehaviour
{

    public List<GameObject> branch;

    GameObject slide;
    GameObject goal;

    const string FirstBranchText = "365일 동안\n난 결국 성공\n하지 못했다.";
    const string SecondBranchText = "부모님과 약속대로\n군대에 가고\n이후 공무원 준비를 ";
    const string ThridBranchText = "엔딩 군대\n다른길도 있었을까?";

    private void OnEnable()
    {
        SearchBranch();
        StartCoroutine(FirstBranch());
    }
    private void SearchBranch()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            branch.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator FirstBranch()
    {
        branch[0].SetActive(true);
        Text send = branch[0].transform.Find("Text").GetComponent<Text>();
        send.text = FirstBranchText;
        GameObject.Find("EndingCanvas").GetComponent<EndingIntro>().EffectText(send);
        yield return new WaitForSeconds(3f);
        StartCoroutine(SecondBranch());
    }
    IEnumerator SecondBranch()
    {
        branch[1].SetActive(true);
       
        goal = branch[1].transform.Find("Text").gameObject;
        slide = branch[1].transform.Find("Slide").gameObject;

        Text send = goal.GetComponent<Text>();
        slide.transform.DOMove(goal.transform.position, 1f);

        yield return new WaitForSeconds(2f);
        send.text = SecondBranchText;
        GameObject.Find("EndingCanvas").GetComponent<EndingIntro>().EffectText(send);
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(ThirdBranch());

    } IEnumerator ThirdBranch()
    {
        branch[2].SetActive(true);

        goal = branch[1].gameObject;
        slide = branch[2].gameObject;

        Text send = branch[2].transform.Find("Video").Find("TextGround").GetChild(0).GetComponent<Text>();
        slide.transform.DOMove(goal.transform.position, 2f);

        yield return new WaitForSeconds(2f);
        send.text = ThridBranchText;
        GameObject.Find("EndingCanvas").GetComponent<EndingIntro>().EffectText(send);

        yield return new WaitForSeconds(5f);
        DOTween.KillAll();

        GameSceneManager.Instance.OnTitle();
    }
}
