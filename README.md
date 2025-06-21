🎮 Flip It! – Memory Match Puzzle Game

Flip It!, Unity ile geliştirilen mobil tabanlı bir hafıza eşleştirme oyunudur. Oyuncular belirli haklar dahilinde kartları eşleştirerek ilerler. Projede baştan sona tüm geliştirme süreci solo olarak yürütülmüş olup, performans, sürdürülebilirlik ve modüler mimari hedeflenmiştir.

🔧 Technologies Used Unity 2022.3.x (LTS)


🧩 Gameplay Özellikleri

- Kart eşleştirme sistemi (ID kontrolü ile 2 eş kart)
- Hak sınırlı, seviye tabanlı ilerleyiş
- Game Over & Level Complete senaryoları
- 2 adet Power-Up:
  - Kartları göster: Tüm kartları kısa süreliğine açar
  - Shuffle: Tüm kartların yerlerini karıştırır
- Smooth animasyonlar, click sesleri ve particle efektleri

🧠 Teknik Mimari

- OOP & SOLID Prensipleri: Tüm sistem `Single Responsibility` ve `Open/Closed` prensiplerine göre ayrıldı
- Event-Driven Sistem: `UnityAction` ile UI, skor ve oyun akışı loosely-coupled şekilde yönetildi
- Modüler Power-Up Sistemi: `abstract` ve `virtual` yapılarla her power-up ayrı sınıfta tanımlandı
- Object Pooling: Kartlar ve efektler için havuz sistemi kurularak Instantiate/Destroy maliyeti sıfırlandı
- DoTween ile Animasyon: Kart açma, kapanma ve UI geçişleri için animasyonlar kullanıldı
- Touch Input: Mobil cihazlarda rahat kontrol için dokunmatik sistemle entegre edildi
- Safe Area & Aspect Ratio: Tüm UI ve kamera yapısı cihaz bağımsız hale getirildi

## 🛠️ Teknolojiler

- Unity (C#)
- DoTween
- UnityEvents (UnityAction)
- Coroutine, PlayerPrefs, Serialized Level System
- Object Pooling, Event-Driven Architecture



![Screenshot 2025-06-17 220912](https://github.com/user-attachments/assets/f6360933-cd1c-4b04-b2e7-8238f762277d)
![Screenshot 2025-06-17 220921](https://github.com/user-attachments/assets/b01ef0fc-4d29-4fd8-b55b-757e458be438)
![Screenshot 2025-06-17 220925](https://github.com/user-attachments/assets/0fd232a5-6ed0-4369-a455-62a8499eb9ad)
![Screenshot 2025-06-17 220936](https://github.com/user-attachments/assets/0a9b3b34-2fe2-4003-9bf8-36450eaa98c6)
