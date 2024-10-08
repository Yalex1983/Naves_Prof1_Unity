using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Networking;

public class ServiciosWeb : MonoBehaviour
{
    public DatosServicioWeb registroUsuario;
    public RespuestaRegistroUsuario respuestaRegistroUsuario;
    void Start()
    {
        DatosRegistroUsuario registro = new DatosRegistroUsuario();
        registro.cedula="197373745";
        registro.email="juan123@hotmail.com";
        registro.nombre ="Pedro";
        StartCoroutine(RegistrarUsuario(registro));
    }

    public IEnumerator RegistrarUsuario(DatosRegistroUsuario registro)
    {
        var respuestaJSON = JsonUtility.ToJson(registro);

        var solicitud = new UnityWebRequest();
        solicitud.url = registroUsuario.url;

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(respuestaJSON);
        solicitud.uploadHandler = new UploadHandlerRaw(bodyRaw);
        solicitud.downloadHandler = new DownloadHandlerBuffer();
        solicitud.method = UnityWebRequest.kHttpVerbPOST;
        solicitud.SetRequestHeader("Content-Type", "application/json");
        
        solicitud.timeout = 10;

        yield return solicitud.SendWebRequest();

        if (solicitud.result == UnityWebRequest.Result.ConnectionError)
        {
            registroUsuario.mensaje = "Conexion fallida";
        }
        else
        {           
            respuestaRegistroUsuario = (RespuestaRegistroUsuario)JsonUtility.FromJson(solicitud.downloadHandler.text, typeof(RespuestaRegistroUsuario));
            registroUsuario.mensaje = respuestaRegistroUsuario.mensaje;
        }
        solicitud.Dispose();
        registroUsuario.evento.Invoke();
    }

}
