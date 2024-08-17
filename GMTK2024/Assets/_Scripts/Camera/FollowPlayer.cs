using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // Refer�ncia ao Transform do jogador
    public Vector3 offset;     // Deslocamento da c�mera em rela��o ao jogador
    public float zoom = -10f;  // Controle do n�vel de zoom no Inspector

    private Camera cam;        // Refer�ncia � c�mera

    void Start()
    {
        cam = GetComponent<Camera>(); // Obt�m a componente Camera do objeto
    }

    void LateUpdate()
    {
        // Atualiza a posi��o da c�mera com o offset e a posi��o do jogador
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, zoom);

        // Define o campo de vis�o ortogr�fico da c�mera com base no zoom
        cam.orthographicSize = Mathf.Abs(zoom);
    }
}
