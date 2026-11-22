using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
[DisallowMultipleComponent]
public class RoundedImage : MonoBehaviour, IMaterialModifier
{
    [Tooltip("Use the same radius for all four corners.")]
    public bool useUniformRadius = true;

    [Tooltip("Corner radius in pixels (applied to all corners).")]
    public float uniformRadius = 20f;

    [Tooltip("Individual corner radii in pixels. x: Top-Left, y: Top-Right, z: Bottom-Right, w: Bottom-Left.")]
    public Vector4 cornerRadii = new Vector4(20f, 20f, 20f, 20f);

    [Tooltip("Width of the border in pixels.")]
    public float borderWidth = 0f;

    [Tooltip("Color of the border.")]
    public Color borderColor = Color.white;

    private Image m_Image;
    private Material m_CustomMaterial;
    private RectTransform m_RectTransform;

    public Image image
    {
        get
        {
            if (m_Image == null)
                m_Image = GetComponent<Image>();
            return m_Image;
        }
    }

    public RectTransform rectTransform
    {
        get
        {
            if (m_RectTransform == null)
                m_RectTransform = GetComponent<RectTransform>();
            return m_RectTransform;
        }
    }

    protected void OnEnable()
    {
        if (image != null)
        {
            image.SetMaterialDirty();
        }
    }

    protected void OnDisable()
    {
        if (image != null)
        {
            image.SetMaterialDirty();
        }
    }

    protected void OnDestroy()
    {
        DestroyMaterial();
    }

    protected void OnValidate()
    {
        // Clamp properties in Inspector
        uniformRadius = Mathf.Max(0f, uniformRadius);
        cornerRadii.x = Mathf.Max(0f, cornerRadii.x);
        cornerRadii.y = Mathf.Max(0f, cornerRadii.y);
        cornerRadii.z = Mathf.Max(0f, cornerRadii.z);
        cornerRadii.w = Mathf.Max(0f, cornerRadii.w);
        borderWidth = Mathf.Max(0f, borderWidth);

        if (image != null)
        {
            image.SetMaterialDirty();
        }
    }

    protected void OnRectTransformDimensionsChange()
    {
        if (image != null)
        {
            image.SetMaterialDirty();
        }
    }

    public Material GetModifiedMaterial(Material baseMaterial)
    {
        if (!enabled)
            return baseMaterial;

        // Find or create custom material using our UI/RoundedImage shader
        if (m_CustomMaterial == null)
        {
            Shader shader = Shader.Find("UI/RoundedImage");
            if (shader == null)
            {
                Debug.LogWarning("UI/RoundedImage shader not found!");
                return baseMaterial;
            }
            m_CustomMaterial = new Material(shader);
            m_CustomMaterial.hideFlags = HideFlags.HideAndDontSave;
        }

        // Copy stencil and clipping properties from baseMaterial (essential for UI Masks to work)
        if (baseMaterial.HasProperty("_Stencil"))
        {
            m_CustomMaterial.SetInt("_Stencil", baseMaterial.GetInt("_Stencil"));
            m_CustomMaterial.SetInt("_StencilComp", baseMaterial.GetInt("_StencilComp"));
            m_CustomMaterial.SetInt("_StencilOp", baseMaterial.GetInt("_StencilOp"));
            m_CustomMaterial.SetInt("_StencilReadMask", baseMaterial.GetInt("_StencilReadMask"));
            m_CustomMaterial.SetInt("_StencilWriteMask", baseMaterial.GetInt("_StencilWriteMask"));
            m_CustomMaterial.SetInt("_ColorMask", baseMaterial.GetInt("_ColorMask"));
        }

        // Update custom material properties
        Rect rect = rectTransform.rect;
        m_CustomMaterial.SetVector("_RectParams", new Vector4(rect.width, rect.height, 0, 0));
        
        Vector4 finalRadii = useUniformRadius 
            ? new Vector4(uniformRadius, uniformRadius, uniformRadius, uniformRadius) 
            : cornerRadii;
        m_CustomMaterial.SetVector("_CornerRadii", finalRadii);
        m_CustomMaterial.SetFloat("_BorderWidth", borderWidth);
        m_CustomMaterial.SetColor("_BorderColor", borderColor);

        return m_CustomMaterial;
    }

    private void DestroyMaterial()
    {
        if (m_CustomMaterial != null)
        {
            #if UNITY_EDITOR
            DestroyImmediate(m_CustomMaterial);
            #else
            Destroy(m_CustomMaterial);
            #endif
            m_CustomMaterial = null;
        }
    }
}