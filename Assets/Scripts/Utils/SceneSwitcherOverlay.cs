#if UNITY_2022_2_OR_NEWER  // => Bu kodun sadece 2022.2 s�r�m ve �st�nde �al��mas�n� sa�lar - Eski s�r�m de hata almamak i�in.
using UnityEditor;  //=> Temel K�t�phane
using UnityEditor.Overlays; //=> OverLays API kullanmak i�in
using UnityEngine; //=> Unity 
using UnityEditor.SceneManagement;  //=> Sahne y�netimi a�ma ve kapama i�in
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor.Build.Content;
using UnityEngine.SceneManagement;
using OpenCover.Framework.Model;
using Unity.VisualScripting;  //=> Dizi y�netimi.

[Overlay(typeof(SceneView),"Scene Switcher",true)] // => Hareketli Panel - sadece SceneView de g�z�kmesi - Panel ismi - varsay�lan Aktif olsun mu ? 
// Scene View de sahne aralar�nda h�zl� ge�i� yapmak i�in
public class SceneSwitcherOverlay : Overlay  // => Overlaydan t�r�yor. 
{
   private VisualElement sceneList;  // => sahbe butonlar�n� tutan ana kapsay�c�
    public override VisualElement CreatePanelContent()
    {
        var root = new VisualElement();

        var refreshBtn = new Button(() =>
        {
            UpdateSceneList();

        })
        {
            text = "Refresh"
        };
       root.Add(refreshBtn);

        sceneList = new VisualElement();
        root.Add(sceneList);

        UpdateSceneList();

        return root;

    }

    private void UpdateSceneList()
    {
        sceneList.Clear();  // Listeyi temizledik ( panel yenilendi�inde eskileri sil )
        // Listeyi g�ncel tutmak ve iki kez eklememek i�in


        var scenes = AssetDatabase.FindAssets("t:scene").Select(AssetDatabase.GUIDToAssetPath).ToArray();
        // => Projede b�t�n .unity uzant�l� sahneleri bulur - GUIDLER� tam dosya yolunu �evirir - Array liste atar.
        // Neden b�yle; B�t�n sahne dosyalar�n� dinamik olarak bulmak i�in.B�ylece yeni sahne ekledik�e panel otomatik g�ncellenir.
        

        foreach (var scenePath in scenes)  // => T�m sahneleri tek tek dola��yor
        {
            var btn = new Button(() =>  // Her Sahne i�in buton olu�turuyoruz 
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())  // A��k olan sahneyi kaydetmek i�in sorar   G�venlik i�in �nemli
                {
                    EditorSceneManager.OpenScene(scenePath); // butona t�klad���nda t�klanan sahne ac�l�r.
                }
            })
            {
                text = System.IO.Path.GetFileNameWithoutExtension(scenePath) // Sahnenin direkt ad�n� yaz�yor Uzant� olmadan
            };
            sceneList.Add(btn); // butonu panele ekliyor

        }
    }

    // UI Toolkit (VisualElement) ile overlay paneli modern, kolayca �zelle�tirilebilir, performansl� ve Unity'nin yeni sistemleriyle uyumlu.
    // Refresh butonu eklemek: AssetDatabase otomatik g�ncellenmedi�i i�in, kullan�c� kolayca g�ncelleyebilsin diye.
    // sceneList kapsay�c�s�: Yaln�zca sahne listesini g�ncelleyerek performansl� ve temiz bir panel aray�z� kurmak i�in.
    // Fonksiyonlar� mod�ler yazmak (UpdateSceneList): Kod okunabilir, kolay geli�tirilebilir ve hatas�z olur.


}

#endif
