using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public Text tile;
    public Text detail;
    public Image choiceImage;

    public bool touch;

    public void ResultPrint(GameObject winner, GameObject choice) //결과 화면 출력 
    {
        choiceImage.sprite = choice.GetComponent<Image>().sprite; //화면에 고른 달팽이를 출력 해준다.

        if (winner == choice) //승리한 달팽이와 고른달팽이가 같다면 
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }
    void WinGame()
    {
        tile.text = "우승";
        tile.color = Color.yellow;
        detail.text = "배팅금액의 2배를\n가져갑니다.";
        detail.color = tile.color;
        transform.parent.Find("GameMoney").GetComponent<GameMoney>().WinGame();
        AudioManager.Sound.Play("SE/snailWin", E_SOUND.SE);
    }
    void LoseGame()
    {
        tile.text = "패배";
        tile.color = Color.red;
        detail.text = "돈을 잃었습니다!";
        detail.color = tile.color; 
        AudioManager.Sound.Play("SE/snailLose", E_SOUND.SE);
    }
    private void Update()
    {
        if(Input.touchCount > 0 )
        {
            gameObject.SetActive(false);
            transform.parent.GetComponent<SnailGame>().NewGame();
        //    Debug.Log("화면 터치 카운트 발동 ");
        }
        if(Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false); 
            transform.parent.GetComponent<SnailGame>().NewGame();
      //      Debug.Log("마우스 클릭 발동  ");
        }
    }
}
