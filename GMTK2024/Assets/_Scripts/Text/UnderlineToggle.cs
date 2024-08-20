using UnityEngine;
using TMPro;

public class UnderlineToggle : MonoBehaviour
{
    [Header("TextMeshPro Component")]
    public TextMeshProUGUI textMeshPro;

    [Header("Toggle Interval")]
    public float toggleInterval = 1.0f; // Intervalo de tempo para alternar o underline (em segundos)

    private bool isUnderlined = false;
    private float timer;

    void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
            this.enabled = false;
            return;
        }

        // Inicializa o timer
        timer = toggleInterval;
        UpdateUnderline();
    }

    void Update()
    {
        // Atualiza o timer
        timer -= Time.deltaTime;

        // Se o timer chegar a zero, alterna o estado do underline
        if (timer <= 0)
        {
            ToggleUnderline();
            timer = toggleInterval; // Reinicia o timer
        }
    }

    void ToggleUnderline()
    {
        isUnderlined = !isUnderlined;
        UpdateUnderline();
    }

    void UpdateUnderline()
    {
        var text = textMeshPro.text;
        var underlineTag = isUnderlined ? "<u>" : "</u>";
        textMeshPro.text = isUnderlined ? $"<u>{text}</u>" : text.Replace("<u>", "").Replace("</u>", "");
    }
}
