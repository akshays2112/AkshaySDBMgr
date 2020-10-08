using UnityEngine;
using UnityEngine.UI;

public class Utilities
{
    public static (float, float) GetTextSize(Text text)
    {
        TextGenerator textGenerator = new TextGenerator();
        TextGenerationSettings tgs = text.GetGenerationSettings(new Vector2(10000, 10000));
        return (textGenerator.GetPreferredWidth(text.text, tgs), textGenerator.GetPreferredHeight(text.text, tgs));
    }
}
