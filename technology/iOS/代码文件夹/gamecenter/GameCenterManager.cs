using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class GameCenterManager : System.Object
{
    private static GameCenterManager instance;
    private static object _lock = new object();

    private GameCenterManager ()
    {
    }

    public static GameCenterManager GetInstance ()
    {
        if (instance == null)
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new GameCenterManager();
                }
            }
        }
        return instance;
    }

    public void Start ()
    {
        UnityEngine.Social.localUser.Authenticate(HandleAuthenticated);
    }

    private void HandleAuthenticated (bool success)
    {
        Debug.Log("*** HandleAuthenticated: success = " + success);
        if (success)
        {
            string userInfo = "UserName:" + UnityEngine.Social.localUser.userName + "\nUser ID:" +
                              UnityEngine.Social.localUser.id + " \nIsUnderage: " + UnityEngine.Social.localUser.underage;
            Debug.Log(userInfo);

            //下面三行看个人需要，需要什么信息就取什么信息，这里注释掉是因为担心有的朋友没有在iTunesConnect里设置排行、成就之类的东西，运行起来可能会报错
            //    Social.localUser.LoadFriends(HandleFriendsLoaded);
            //    Social.LoadAchievements(HandleAchievementsLoaded);
            //    Social.LoadAchievementDescriptions(HandleAchievementDescriptionsLoaded);
//            UnityEngine.Social.LoadScores("sixcornerhighscore", scores =>
//			                  {
//				if (scores.Length > 0)
//				{
//					Debug.Log("scores" + scores[0].formattedValue);
//					for(int i = 0; i < scores.Length; i++){
//                        if (scores[i].userID == UnityEngine.Social.localUser.id)
//                        {
//                            Debug.Log(" sixcornerhighscore usedid : " + UnityEngine.Social.localUser.id + ", score : " + scores[i].value);
//							Global.MaxScore = (int)scores[i].value;
//							Global.Rank = i + 1;
//						}
//					}
//				}
//			});
        }
    }

    private void HandleFriendsLoaded (bool success)
    {
        Debug.Log("*** HandleFriendsLoaded: success = " + success);
        foreach (IUserProfile friend in UnityEngine.Social.localUser.friends)
        {
            Debug.Log("* friend = " + friend.ToString());
        }
    }

    private void HandleAchievementsLoaded (IAchievement[] achievements)
    {
        Debug.Log("* HandleAchievementsLoaded");
        foreach (IAchievement achievement in achievements)
        {
            Debug.Log("* achievement = " + achievement.ToString());
        }
    }

    private void HandleAchievementDescriptionsLoaded (IAchievementDescription[] achievementDescriptions)
    {
        Debug.Log("*** HandleAchievementDescriptionsLoaded");
        foreach (IAchievementDescription achievementDescription in achievementDescriptions)
        {
            Debug.Log("* achievementDescription = " + achievementDescription.ToString());
        }
    }

    // achievements
    public void ReportProgress (string achievementId , double progress)
    {
        if (UnityEngine.Social.localUser.authenticated)
        {
            UnityEngine.Social.ReportProgress(achievementId, progress, HandleProgressReported);
        }
    }

    private void HandleProgressReported (bool success)
    {
        Debug.Log("*** HandleProgressReported: success = " + success);
    }

    public void ShowAchievements ()
    {
        if (UnityEngine.Social.localUser.authenticated)
        {
            UnityEngine.Social.ShowAchievementsUI();
        }
    }

    // leaderboard

    public void ReportScore (string leaderboardId , long score)
    {
        if (UnityEngine.Social.localUser.authenticated)
        {
            UnityEngine.Social.ReportScore(score, leaderboardId, HandleScoreReported);
        }
    }

    public void HandleScoreReported (bool success)
    {
        Debug.Log("*** HandleScoreReported: success = " + success);
    }

    public void ShowLeaderboard ()
    {
        Debug.Log("++++++++++++ShowLeaderboardUI");
        if (Social.localUser.authenticated)
        {
            Debug.Log("+++++++++++ShowLeaderboardUI!!!!!!!!!!!!!!");
            UnityEngine.Social.ShowLeaderboardUI();
        }
    }

}