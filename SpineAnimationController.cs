using UnityEngine;
using Spine.Unity;

public class SpineAnimationController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public string idleNoSmile = "Default (Without Smile)_01"; 
    public string idleSmile = "Default (With Smile)_02";    // �������߰ʵe
    public string walkAnimation = "walk";  // �����ʵe
    public string runAnimation = "run";    // �]�B�ʵe

    private bool isMoving = false; // �O�_���b����

    void Start()
    {
        if (skeletonAnimation == null)
            skeletonAnimation = GetComponent<SkeletonAnimation>();

        // �w�]���񯸥߰ʵe�]�H����ܡ^
        PlayRandomIdle();
    }

    void Update()
    {
        // �o�̪� Input �u�O�d�ҡA�ЮھڧA���C����ڱ���覡�ӧP�_���Ⲿ��
        float move = Input.GetAxisRaw("Horizontal");
        if (move != 0)
        {
            if (!isMoving)
            {
                isMoving = true;
                PlayWalk();
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                PlayRandomIdle(); // ���U�ӫ��H���� Idle
            }
        }

        // ���U A/D �� ���k��A���������ʵe
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Walk_01", true);
        }
        // ���U Shift + A/D�A�����]�B�ʵe (�ȮɨS�����ʵe)
        else if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "", true);
        }
        // �S������ɡA�^�� idle�]���ߡ^�ʵe
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Default (Without Smile)_01", true);
        }
    }

    void PlayRandomIdle()
    {
        // �H����ܤ@�� Idle �ʵe
        string chosenIdle = (Random.value > 0.5f) ? idleSmile : idleNoSmile;

        // ���� Idle �ʵe
        skeletonAnimation.AnimationState.SetAnimation(0, chosenIdle, true);
    }
}
{

public string blinkAnimation = "EyeBlink_01";  // �w���ʵe
private float nextBlinkTime = 0f;  // �]�w�U�@���w���ɶ�

void Start()
{
    if (skeletonAnimation == null)
        skeletonAnimation = GetComponent<SkeletonAnimation>();

    // �w�]���� Idle�]���ߡ^�ʵe�b Track 0
    skeletonAnimation.AnimationState.SetAnimation(0, idleAnimation, true);

    // �]�w�H�����즸�w���ɶ�
    ScheduleNextBlink();
}

void Update()
{
    // �ˬd�O�_��F�w���ɶ�
    if (Time.time >= nextBlinkTime)
    {
        Blink();
        ScheduleNextBlink(); // �]�w�U�@���w��
    }
}

void Blink()
{
    // �]�w�H���ʵe�t�ס]0.8x ~ 1.2x�^
    skeletonAnimation.timeScale = Random.Range(0.8f, 1.2f);

    // ����w���ʵe�b Track 1
    skeletonAnimation.AnimationState.SetAnimation(1, blinkAnimation, false);
}

void ScheduleNextBlink()
{
    // �]�w�U�@���w���ɶ��]�H�����j 2~5 ��^
    nextBlinkTime = Time.time + Random.Range(2f, 5f);
}
}