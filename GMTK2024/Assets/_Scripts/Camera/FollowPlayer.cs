using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // Referência ao Transform do jogador
    public Vector3 offset;     // Deslocamento da câmera em relação ao jogador
    public float zoom = -10f;  // Controle do nível de zoom no Inspector

    private Camera cam;        // Referência à câmera

    void Start()
    {
        cam = GetComponent<Camera>(); // Obtém a componente Camera do objeto
    }

    void LateUpdate()
    {
        // Atualiza a posição da câmera com o offset e a posição do jogador
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, zoom);

        // Define o campo de visão ortográfico da câmera com base no zoom
        cam.orthographicSize = Mathf.Abs(zoom);
    }
}
