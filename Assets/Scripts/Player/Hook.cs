/* Esse é o Script do Hook.
 * A ideia aqui é acionar o Hook por um botão de ação, já previamente definido.
 * O Hook lança uma "corda" até o alvo, estipulado pelo curso (mouse).
 * A corda em contato com o primeiro objeto de colição se prende na superfície dele.
 * Uma vez que o Hook acerta algum objeto, o jogador é "puxado" na direção da corda.
 * Uma vez que o jogador deixa de precionar o botão de ação do Hook, a corda se desfaz.
 * Enquanto o jogador não estiver com o Hook ativo, cairá.
 * 
 * OBS: Variáveis importante!! (Para interesse de futuras alterações).
 * Velocidade do PUXO -> step.
 * Distância MÁXIMA da corda -> distancia.
 * 
 * OBS2: Planejamento (possível):
 * Não foi implementado CD (countdown) para o Hook, logo o jogador pode acionar o potão de ação seguidas vezes.
 * Talvez os DGs queriam que o Hook não puxe o jogador até o ponto de colisão.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

    public GameObject player; // FIXME: WORKAROUND FOR RIGIDBODY2D + OWN PLAYER PHYSICS

    public float distancia = 10f; // Variável para definir distancia máxima.
    public float step = 0.2f; // Variável auxiliar para velocidade de puxo.
    public LayerMask mask; // Variável para definir os objetos de colisão
    public LineRenderer line; // Variável para associar a corda do Hook.

    DistanceJoint2D joint; // Nossa variável para ser associada ao componente "Distance Joint 2D"/ jogador.
    Vector3 alvo; // Variável alvo/target.
    RaycastHit2D hit; // Variável auxiliar para definição do momento de impacto.

	// Use this for initialization
	void Start () {
        joint = GetComponent<DistanceJoint2D> (); // Para associar a variável ao componete.
        joint.enabled = false; // Situação Quo "Não está usando o Hook".
        line.enabled = false; // Situação Quo "Não está usando o Hook", logo não existe corda.
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, this.distancia);
    }
	
	// Update is called once per frame
	void Update () {
        if (joint.distance > 1f && joint.enabled) // Se houver espaço entre o jogador e o obejto de colisão...
        {
            joint.distance -= step; // Altere a distância, "puxando" o jogador até o ponto de impacto.

             // FIXME: WORKAROUND FOR RIGIDBODY2D + OWN PLAYER PHYSICS
            player.transform.position = (transform.TransformPoint(joint.anchor)
                                        - joint.connectedBody.transform.TransformPoint(joint.connectedAnchor)).normalized
                                        * joint.distance
                                        + joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);
                                        
        }
        else { // Caso contrário...
            line.enabled = false; // Estado Quo da corda.
            joint.enabled = false; // Estado Quo do jogador.
        }  
		if (Input.GetKeyDown (KeyBindings.Instance.playerHook)) // Se o botão default para acionar o Hook foi precionado...
        {
            alvo = Camera.main.ScreenToWorldPoint (Input.mousePosition); // O alvo vai ser definido pelo mouse (Temporário).
            alvo.z = 0; // O vetor.z é sempre 0.

            hit = Physics2D.Raycast (transform.position, alvo - transform.position, distancia, mask); // Definição de colisão.
            Debug.DrawRay(transform.position, alvo - transform.position, Color.green);

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D> () != null) // Se colidiu com algo...
            {
                joint.enabled = true; // Neste instânte, o Hook foi acionado e colidiu com algo.
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>(); // Conecta nosso personagem pelo Hook.
                joint.connectedAnchor = hit.collider.transform.InverseTransformPoint(hit.point);  // O Hook vais e fixar na borda do Objeto atingido.
                joint.distance = Vector2.Distance(transform.position, hit.point); // Mantém a distância de prisão constante.

                line.enabled = true; // Neste instânte, o Hook foi acionado e colidiu com algo com distancia grande o suficiente para existir a corda.
                line.SetPosition (0, transform.position); // Inicializa o lançamento da corda.
                line.SetPosition (1, hit.point); // Lança a corda até a posição alvo/ de impacto.

            }
        }
        if (Input.GetKey (KeyBindings.Instance.playerHook)) // Se soltar o Hook...
            line.SetPosition (0, transform.position); // Corda volta.

        if(Input.GetKeyUp (KeyBindings.Instance.playerHook)) // Se soltar o botão...
        {
            joint.enabled = false; // Volta à situação Quo.
            line.enabled = false; // Volta à situação Quo.
        }
	}
}
