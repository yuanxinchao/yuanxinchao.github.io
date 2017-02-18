## Resources.Load<Sprite>("7")
         GameObject prueba = new GameObject ("prueba");
         SpriteRenderer renderer= prueba.AddComponent<SpriteRenderer> ();
         Object [] sprites;
         sprites = Resources.LoadAll ("Tiles/Castillo");
         renderer.sprite = (Sprite)sprites [3];