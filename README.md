
👨‍💻 PANDUAN UNTUK SEMUA ANGGOTA TIM

✅ 1. Clone Project dari GitHub

Langkah awal untuk semua anggota, hanya dilakukan sekali:

git clone https://github.com/mbok-rondo/mbok-rondo-game.git

cd mbok-rondo-game

🔁 2. Ambil Update Terbaru dari master (Setiap Hari)

Agar project lokal kamu selalu sesuai dengan pekerjaan terbaru dari tim lain:

git checkout master

git pull origin master

🌿 3. Pindah ke Branch Pribadi

Pindah ke branch kamu sendiri untuk mengerjakan tugas:

git checkout nama-kamu
Misal:

git checkout yudha

🎨 4. Kerjakan Fitur Kamu

Lakukan perubahan di Unity seperti biasa. Setelah selesai (bisa harian atau tiap milestone kecil):

⬆️ 5. Simpan dan Push Pekerjaan ke Branch Sendiri

Pastikan selalu menyimpan pekerjaan dan mem-push setiap perubahan yang kamu buat:

git add .

git commit -m "Deskripsi perubahan, contoh: Tambah animasi NPC wanita"

git push origin nama-branch-kamu
Misal:

git push origin yudha

🔄 6. Setelah Admin Merge, Ambil Update Terbaru Lagi dari master

Setelah Admin menggabungkan perubahan ke master, lakukan langkah berikut untuk memastikan branch kamu selalu up-to-date.

Pertama, pastikan berada di branch master:

git checkout master

git pull origin master

Kemudian, pindah ke branch pribadi kamu:

git checkout nama-kamu

Gunakan rebase daripada merge untuk menghindari commit merge yang tidak perlu, sehingga riwayat commit tetap bersih dan linear:

git rebase master

Dengan rebase, kamu mengaplikasikan perubahan dari master ke branch pribadi kamu. Jika ada konflik, selesaikan konflik tersebut, dan lanjutkan rebase dengan perintah:

git rebase --continue

Setelah selesai, jangan lupa untuk mem-push perubahan kamu:

git push origin nama-branch-kamu --force-with-lease

Catatan: Menggunakan --force-with-lease untuk memaksa push setelah rebase, tetapi dengan aman tanpa menimpa perubahan yang ada di remote.

🧑‍💼 PANDUAN UNTUK ADMIN (NABIL)

✅ 1. Ambil Semua Update dari Semua Branch

Ambil semua update terbaru dari remote repository:

git fetch --all

🔍 2. Cek dan Uji Setiap Branch Sebelum Merge

Pastikan untuk memeriksa setiap branch dengan seksama sebelum menggabungkannya ke master. Uji setiap fitur untuk memastikan tidak ada bug.

🔄 3. Merge ke Branch master

Pastikan master sudah up-to-date sebelum melakukan merge:

git checkout master

git pull origin master # pastikan master versi terbaru

Lalu, merge setiap branch ke dalam master satu per satu:

git merge yazid

git merge hilmy

git merge yudha

git merge echo

git merge nabil

⬆️ 4. Push ke Remote master

Setelah semua branch digabungkan, push perubahan ke remote:

git push origin master

📣 5. Beritahu Semua Anggota untuk Update Project

Setelah semua perubahan berhasil di-merge ke master, beri tahu anggota untuk melakukan update di project mereka dengan langkah berikut:

Ambil perubahan terbaru dari master:

git checkout master

git pull origin master

Pindah ke branch pribadi masing-masing:

git checkout nama-branch-kamu

Update branch pribadi dengan perubahan terbaru dari master menggunakan rebase:

git rebase master

Push perubahan ke remote setelah rebase:


git push origin nama-branch-kamu --force-with-lease

contoh


git push origin yudha --force-with-lease


-----------------------------------------------------

Poin-poin penting yang telah diperbaiki:

Mengganti git merge master dengan git rebase master untuk menjaga riwayat commit tetap bersih dan menghindari "merge commits" yang tidak perlu.

Menggunakan git push --force-with-lease setelah rebase untuk menghindari kesalahan ketika melakukan push.

Menambahkan langkah-langkah untuk memastikan master selalu up-to-date sebelum melakukan merge di branch pribadi dan remote.

Menekankan pentingnya melakukan rebase dibanding merge setelah perubahan di-merge ke master, agar branch pribadi selalu linier dengan master.

Dengan mengikuti panduan yang lebih jelas dan terstruktur ini, diharapkan akan mengurangi kemungkinan konflik Git dan membuat alur kerja tim menjadi lebih lancar.
