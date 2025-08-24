document.getElementById('showUnplayedOnly').addEventListener('change', function () {
    const showOnly = this.checked;
    const rows = document.querySelectorAll('#songsTable tbody tr');
    rows.forEach(row => {
        if (showOnly && row.dataset.played === 'yes') {
            row.style.display = 'none';
        } else {
            row.style.display = '';
        }
    });
});
