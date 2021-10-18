using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public Animator groupHighScore;
    public bool highScoreOpen = false;

    public Animator groupPlay;
    public bool playOpen = false;

    public Animator groupAvatar;
    public bool avatarOpen = false;
    

    public void HighScoreOpen()
    {

        if (highScoreOpen)
        {
            //close
            groupHighScore.Play("HighScoreClose");
            highScoreOpen = false;
        }
        else
        {
            //open
            groupHighScore.Play("HighScoreOpen");
            highScoreOpen = true;
        }
    }
    public void OpenPlayButton()
    {
        if (playOpen)
        {
            //close
            groupPlay.Play("PlayDisappear");
            playOpen = false;
        }
        else
        {
            //open
            groupPlay.Play("PlayAppear");
            playOpen = true;
        }
    }
    public void DisplayAvatar()
    {
        if (avatarOpen)
        {
            //close
            groupAvatar.Play("AvatarClose");
            avatarOpen = false;
        }
        else
        {
            //open
            groupAvatar.Play("AvatarOpen");
            avatarOpen = true;
        }
    }
    
}
