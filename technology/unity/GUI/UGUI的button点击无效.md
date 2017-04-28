## UGUI的button点击无效自测
- Make sure the graphic has "raycastTarget" set to True.
- Make sure the button has its "interactable" set to True.
- If there's a nested canvas in the button's parent, that canvas also needs GraphicsRaycaster.

If all those are fine, then start the game, select your EventSystem and click your button. At the bottom of the inspector you'll see wha