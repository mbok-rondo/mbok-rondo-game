# mbok-rondo-game

# 🎮 Mbok-Rondo Game

**Mbok-Rondo** adalah game yang dikembangkan menggunakan Unity oleh mahasiswa Teknik Informatika dalam rangka proyek kelompok mata kuliah *Game Product Development*. Game ini akan dikembangkan secara kolaboratif menggunakan GitHub dengan metode pembagian tugas berdasarkan branch masing-masing.

---

## 👥 Anggota Tim Pengembang

| Nama     | Role / Branch         |
|----------|-----------------------|
| Nabil    | Koordinator / `master` |
| Nabil    | Developer / `nabil` |
| Yazid    | Developer / `yazid` |
| Hilmy    | Developer / `hilmy` |
| Echo     | Developer / `echo` |
| Yudha    | Developer / `yudha` |

---

## 🌱 Struktur Branch

- `master` : Branch utama, hanya di-*merge* oleh Nabil sebagai admin.
- `fitur-[nama]` : Branch untuk masing-masing anggota tim berdasarkan fitur yang sedang dikerjakan.

---

## 🔄 Alur Kerja Kolaborasi

1. **Setiap anggota bekerja di branch-nya sendiri**:
   - Misal Yudha bekerja di `yudha`
2. **Setiap selesai bekerja harian**, push ke branch masing-masing:
   ```bash
   git add .
   git commit -m "Deskripsi singkat update"
   git push origin fitur-nama

## 👨‍💻 PANDUAN UNTUK SEMUA ANGGOTA TIM

✅ 1. Clone Project dari GitHub
Langkah awal untuk semua anggota, hanya dilakukan sekali


  git clone https://github.com/mbok-rondo/mbok-rondo-game.git 
  cd mbok-rondo-game
  
🔁 2. Ambil Update Terbaru dari master (Setiap Hari)
Agar project lokal kamu selalu sesuai dengan pekerjaan terbaru dari tim lain:


  git checkout master

  
  git pull origin master

  
  
🌿 3. Pindah ke Branch Pribadi
Pindah ke branch kamu sendiri untuk mengerjakan tugas:


  git checkout nama-kamu

  
  misal git checkout yudha

  
🎨 4. Kerjakan Fitur Kamu
Lakukan perubahan di Unity seperti biasa. Setelah selesai (bisa harian atau tiap milestone kecil):

⬆️ 5. Simpan dan Push Pekerjaan ke Branch Sendiri


  git add .
  
  git commit -m "Deskripsi perubahan, contoh: Tambah animasi NPC wanita"
  
  git push origin fitur-nama-kamu
  
  misal git push origin yudha
  
  
🔄 6. Setelah Admin Merge, Ambil Update Terbaru Lagi dari master

  git checkout master
  
  git pull origin master

  
Kemudian gabungkan ke branch kamu sendiri agar up-to-date:

  git checkout -nama-kamu
  
  git merge master




## 🧑‍💼 PANDUAN UNTUK ADMIN (NABIL)

**✅ 1. Ambil Semua Update dari Semua Branch

  git fetch --all

  
🔍 2. Cek dan Uji Setiap Branch Sebelum Merge


🔄 3. Merge ke Branch master

  git checkout master
  
  git pull origin master  # pastikan master versi terbaru

  
Lalu merge satu per satu

  git merge yazid
  
  git merge hilmy
  
  git merge yudha
  
  git merge echo
  
  git merge nabil
  
  
⬆️ 4. Push ke Remote master

git push origin master


📣 5. Beritahu Semua Anggota untuk Update Project

Anggota tinggal melakukan:

  git checkout master
  
  git pull origin master
  
  git checkout fitur-nama
  
  git merge master











