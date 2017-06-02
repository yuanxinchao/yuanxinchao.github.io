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

更好的实现：

    private void DoEffectAddNum(Text text, long originNum, long nowNum)
    {
        text.transform.DOPause();
        text.transform.DOScale(new Vector3(1.5f, 1.5f, 0), 0.2f).OnComplete(() =>
        {
            text.transform.DOScale(new Vector3(1, 1, 0), 0.3f);
        });
        DOTween.To(() => originNum, x => { originNum = x; text.text = originNum.Tostring(); }, nowNum, 0.5f);
    }