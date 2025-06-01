#if UNITY_2022_2_OR_NEWER  // => Bu kodun sadece 2022.2 sürüm ve üstünde çalýþmasýný saðlar - Eski sürüm de hata almamak için.
using UnityEditor;  //=> Temel Kütüphane
using UnityEditor.Overlays; //=> OverLays API kullanmak için
using UnityEngine; //=> Unity 
using UnityEditor.SceneManagement;  //=> Sahne yönetimi açma ve kapama için
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor.Build.Content;
using UnityEngine.SceneManagement;
using OpenCover.Framework.Model;
using Unity.VisualScripting;  //=> Dizi yönetimi.

[Overlay(typeof(SceneView),"Scene Switcher",true)] // => Hareketli Panel - sadece SceneView de gözükmesi - Panel ismi - varsayýlan Aktif olsun mu ? 
// Scene View de sahne aralarýnda hýzlý geçiþ yapmak için
public class SceneSwitcherOverlay : Overlay  // => Overlaydan türüyor. 
{
   private VisualElement sceneList;  // => sahbe butonlarýný tutan ana kapsayýcý
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
        sceneList.Clear();  // Listeyi temizledik ( panel yenilendiðinde eskileri sil )
        // Listeyi güncel tutmak ve iki kez eklememek için


        var scenes = AssetDatabase.FindAssets("t:scene").Select(AssetDatabase.GUIDToAssetPath).ToArray();
        // => Projede bütün .unity uzantýlý sahneleri bulur - GUIDLERÝ tam dosya yolunu çevirir - Array liste atar.
        // Neden böyle; Bütün sahne dosyalarýný dinamik olarak bulmak için.Böylece yeni sahne ekledikçe panel otomatik güncellenir.
        

        foreach (var scenePath in scenes)  // => Tüm sahneleri tek tek dolaþýyor
        {
            var btn = new Button(() =>  // Her Sahne için buton oluþturuyoruz 
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())  // Açýk olan sahneyi kaydetmek için sorar   Güvenlik için önemli
                {
                    EditorSceneManager.OpenScene(scenePath); // butona týkladýðýnda týklanan sahne acýlýr.
                }
            })
            {
                text = System.IO.Path.GetFileNameWithoutExtension(scenePath) // Sahnenin direkt adýný yazýyor Uzantý olmadan
            };
            sceneList.Add(btn); // butonu panele ekliyor

        }
    }

    // UI Toolkit (VisualElement) ile overlay paneli modern, kolayca özelleþtirilebilir, performanslý ve Unity'nin yeni sistemleriyle uyumlu.
    // Refresh butonu eklemek: AssetDatabase otomatik güncellenmediði için, kullanýcý kolayca güncelleyebilsin diye.
    // sceneList kapsayýcýsý: Yalnýzca sahne listesini güncelleyerek performanslý ve temiz bir panel arayüzü kurmak için.
    // Fonksiyonlarý modüler yazmak (UpdateSceneList): Kod okunabilir, kolay geliþtirilebilir ve hatasýz olur.


}

#endif
