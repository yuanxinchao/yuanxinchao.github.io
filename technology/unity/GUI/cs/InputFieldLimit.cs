using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputFieldLimit : MonoBehaviour
{
    [SerializeField]
    private int _charLimit = 40; //字符限制,中文2个字符,英文1个字符

    private InputField _inputField;

    void Awake()
    {
        _inputField = GetComponent<InputField>();
        _inputField.characterLimit = _charLimit;
        if (_charLimit > 0)
        {
            _inputField.onValidateInput += OnValidateInput;
        }
    }

    private char OnValidateInput(string text, int charIndex, char addedChar)
    {
        int n = 0;
        for (int i = 0; i < text.Length; i++)
        {
            n += GetCount(text[i]);
        }
        n += GetCount(addedChar);
        if (char.GetUnicodeCategory(addedChar) == UnicodeCategory.Surrogate)
        {
            return '\0';
        }
        if ( n > _charLimit)
        {
            return '\0';
        }
        return addedChar;
    }

    private int GetCount(char c)
    {
        //中文2个字符,其它1个字符,暂时不考虑其他语言
        if (c >= 0x4e00 && c < 0x9fbb)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
