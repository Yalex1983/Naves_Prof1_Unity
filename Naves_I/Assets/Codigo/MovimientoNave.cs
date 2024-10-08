using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoNave : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public Rigidbody cuerpoRigido;
    private Vector3 movimiento;

    void Start()
    {
        cuerpoRigido = GetComponent<Rigidbody>();  
    }

    void Update()
    {
       float moverX = Input.GetAxis("Horizontal");
       float moverZ = Input.GetAxis("Vertical");

       movimiento = new Vector3(moverX, moverZ, 0); 
    }
    void FixedUpdate()
    {
        Vector3 nuevaPosicion = cuerpoRigido.position + movimiento * velocidadMovimiento * Time.fixedDeltaTime;
        cuerpoRigido.MovePosition(nuevaPosicion);
    }

}
