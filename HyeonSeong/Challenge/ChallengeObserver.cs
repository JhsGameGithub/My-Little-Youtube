using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChallengeObserver : MonoBehaviour
{
    public List<ChallengeSlot> challenge_slot;
    private bool[] is_achieve = new bool[38];
    private int[] challenge_limit = new int[38];

    private void Awake()
    {
        #region achieve limit 초기화
        challenge_limit[0] = 1;// 첫번째 방송
        challenge_limit[1] = 100;// 시청자 100명 돌파
        challenge_limit[2] = 1000;// 시청자 1000명 돌파
        challenge_limit[3] = 10000;// 시청자 10000명 돌파
        challenge_limit[4] = 1;// 첫번째 영상
        challenge_limit[5] = 10000;// 누적 조회수 10000회 돌파
        challenge_limit[6] = 100000;// 누적 조회수 100000회 돌파
        challenge_limit[7] = 1000000;// 누적 조회수 1000000회 돌파
        challenge_limit[8] = 10000000;// 누적 조회수 10000000회 돌파
        challenge_limit[9] = 1;// 첫번째 구독자
        challenge_limit[10] = 1000;// 구독자 1000명돌파
        challenge_limit[11] = 50000;// 구독자 50000명 돌파
        challenge_limit[12] = 500000;// 구독자 500000명 돌파
        challenge_limit[13] = 100000;// 실버버튼 달성!
        challenge_limit[14] = 1000000;// 골드버튼 달성!
        challenge_limit[15] = 1000000;// 내 왼손에는 백만원
        challenge_limit[16] = 10000000;// 내 오른손에는 천만원
        challenge_limit[17] = 100000000;// 님 유튜브 좀 쩌는듯
        challenge_limit[18] = 30;// 헬창
        challenge_limit[19] = 20;// 슈퍼 컴퓨터
        challenge_limit[20] = 20;// 포션 중독자
        challenge_limit[21] = 15;// 컨텐츠 뱅크
        challenge_limit[22] = 4;// 주인님
        challenge_limit[23] = 20;// 일류 셰프
        challenge_limit[24] = 5;// 별이 다섯개!
        challenge_limit[25] = 20;// 엘데의 왕
        challenge_limit[26] = 20;// 잼민이의 우상
        challenge_limit[27] = 20;// 나 메이플 만렙이야
        challenge_limit[28] = 20;// 님 섬광 매너요
        challenge_limit[29] = 20;// 외계인 학살자
        challenge_limit[30] = 20;// 님 누 몇
        challenge_limit[31] = 20;// 마지막 장작
        challenge_limit[32] = 20;// 돌깎기의 달인
        challenge_limit[33] = 20;// 치킨 중독자
        challenge_limit[34] = 20;// 장래희망 카이바
        challenge_limit[35] = 20;// 호글린의 왕
        challenge_limit[36] = 20;// 눈물 마스터
        challenge_limit[37] = 20;// 킹 뱀파이어
        #endregion
    }
    private void Start()
    {
        ChallengeBroker.Instance.challenge_observer = this;
    }

    // 초기화 함수
    public void Init(List<ChallengeSlot> challenge_slot)
    {
        //ui에 있는 슬롯들 가져오기
        this.challenge_slot = challenge_slot;
        //불러온 슬롯들을 achieve 수치에 따라 ui 업데이트
        for (int i = 0; i < challenge_slot.Count; i++)
            Unlock_Challenge(i, true);
    }

    //달성한 도전과제 UI 바꾸기
    public void Unlock_Challenge(int index, bool is_load, int achieve = 0)
    {
        //불러오기면 달성도 검사, 아니라면 변경한 달성도 검사
        //달성도가 도전과제 목표보다 높다면 UI 변경
        //불러오기가 아니라면 변경 알림 보내기
        if (!is_achieve[index] && ((is_load ? challenge_slot[index].info.achieve : (challenge_slot[index].info.achieve += achieve)) >= challenge_limit[index])) 
        {
            challenge_slot[index].SetUI();
            is_achieve[index] = true;
            if (!is_load)
            {
                //알림 코드
                AudioManager.Sound.Play("SE/Challenge", E_SOUND.SE);
                PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);
                popupBuilder.SetDescription("도전과제 달성");
                popupBuilder.SetAnim("FadeIn", "FadeOut");
                popupBuilder.SetAutoDestroy(true);
                popupBuilder.Build("AchievementsPopupUI");
            }
        }
    }
}
