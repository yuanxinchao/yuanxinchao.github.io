using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XcText : MaskableGraphic
{
    [SerializeField]
    private FontData m_FontData = FontData.defaultFontData;

    readonly UIVertex[] m_TempVerts = new UIVertex[4];

    [TextArea(3, 10)]
    [SerializeField]
    protected string m_Text = string.Empty;
    public Font font
    {
        get
        {
            return m_FontData.font;
        }
        set
        {
            if (m_FontData.font == value)
                return;
            m_FontData.font = value;
            SetAllDirty();
        }
    }

    public virtual string text
    {
        get
        {
            return m_Text;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                if (string.IsNullOrEmpty(m_Text))
                    return;
                m_Text = "";
                SetVerticesDirty();
            }
            else if (m_Text != value)
            {
                m_Text = value;
                SetVerticesDirty();
                SetLayoutDirty();
            }
        }
    }
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        if (font == null)
            return;

        // We don't care if we the font Texture changes while we are doing our Update.
        // The end result of cachedTextGenerator will be valid for this instance.
        // Otherwise we can get issues like Case 619238.


        Vector2 extents = rectTransform.rect.size;

        var settings = GetGenerationSettings(extents);
        TextGenerator generator = new TextGenerator();
        generator.PopulateWithErrors(text, settings, gameObject);
        // Apply the offset to the vertices
        IList<UIVertex> verts = generator.verts;
        float unitsPerPixel = 1 / pixelsPerUnit;
        //Last 4 verts are always a new line... (\n)
        int vertCount = verts.Count - 4;

        Vector2 roundingOffset = new Vector2(verts[0].position.x, verts[0].position.y) * unitsPerPixel;
        roundingOffset = PixelAdjustPoint(roundingOffset) - roundingOffset;
        toFill.Clear();
        if (roundingOffset != Vector2.zero)
        {
            for (int i = 0; i < vertCount; ++i)
            {
                
                int tempVertsIndex = i & 3;
                m_TempVerts[tempVertsIndex] = verts[i];
                m_TempVerts[tempVertsIndex].position *= unitsPerPixel;
                m_TempVerts[tempVertsIndex].position.x += roundingOffset.x;
                m_TempVerts[tempVertsIndex].position.y += roundingOffset.y;
                if (tempVertsIndex == 3)
                {

                    toFill.AddUIVertexQuad(m_TempVerts);
                }
            }
        }
        else
        {
            for (int i = 0; i < vertCount; ++i)
            {
                int tempVertsIndex = i & 3;
                m_TempVerts[tempVertsIndex] = verts[i];
                m_TempVerts[tempVertsIndex].position *= unitsPerPixel;

                m_TempVerts[tempVertsIndex].position += new Vector3(0, i/4 * -10, 0);//倾斜
               
                if (tempVertsIndex == 3)
                    toFill.AddUIVertexQuad(m_TempVerts);
            }
        }
    }
    public TextGenerationSettings GetGenerationSettings(Vector2 extents)
    {
        var settings = new TextGenerationSettings();

        settings.generationExtents = extents;
        if (font != null && font.dynamic)
        {
            settings.fontSize = m_FontData.fontSize;
            settings.resizeTextMinSize = m_FontData.minSize;
            settings.resizeTextMaxSize = m_FontData.maxSize;
        }

        // Other settings
        settings.textAnchor = m_FontData.alignment;
        settings.alignByGeometry = m_FontData.alignByGeometry;
        settings.scaleFactor = pixelsPerUnit;
        settings.color = color;
        settings.font = font;
        settings.pivot = rectTransform.pivot;
        settings.richText = m_FontData.richText;
        settings.lineSpacing = m_FontData.lineSpacing;
        settings.fontStyle = m_FontData.fontStyle;
        settings.resizeTextForBestFit = m_FontData.bestFit;
        settings.updateBounds = false;
        settings.horizontalOverflow = m_FontData.horizontalOverflow;
        settings.verticalOverflow = m_FontData.verticalOverflow;

        return settings;
    }
    public float pixelsPerUnit
    {
        get
        {
            var localCanvas = canvas;
            if (!localCanvas)
                return 1;
            // For dynamic fonts, ensure we use one pixel per pixel on the screen.
            if (!font || font.dynamic)
                return localCanvas.scaleFactor;
            // For non-dynamic fonts, calculate pixels per unit based on specified font size relative to font object's own font size.
            if (m_FontData.fontSize <= 0 || font.fontSize <= 0)
                return 1;
            return font.fontSize / (float)m_FontData.fontSize;
        }
    }
    public override Texture mainTexture
    {
        get
        {
            if (font != null && font.material != null && font.material.mainTexture != null)
                return font.material.mainTexture;

            if (m_Material != null)
                return m_Material.mainTexture;

            return base.mainTexture;
        }
    }
}
