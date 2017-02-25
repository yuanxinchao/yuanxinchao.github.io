## UGUI Text 实现数值缓慢增加显示
	StartCoroutine(TotalScoreChange(tempScore, aidScore));
	private IEnumerator TotalScoreChange (int tempScore , int aidScore)
    {
        DOTween.To(() => tempScore, x => tempScore = x, aidScore, 0.5f);
        while (tempScore != aidScore)
        {
            yield return 1;
            Debug.Log(tempScore);
            TotalScoreText.text = tempScore.ToString();
        }
    }