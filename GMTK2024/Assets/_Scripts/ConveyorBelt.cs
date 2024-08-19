using UnityEngine;
public class ConveyorBelt : MonoBehaviour
{
    public float conveyorSpeed = 2f; // Velocidade da esteira
    public bool moveRight = true; // Define a direção da esteira (direita ou esquerda)

    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            // Defina a direção do movimento
            Vector2 direction = moveRight ? Vector2.right : Vector2.left;

            // Aplique uma força no Rigidbody2D do jogador
            rb.velocity = new Vector2(conveyorSpeed * direction.x, rb.velocity.y);
        }
    }
}