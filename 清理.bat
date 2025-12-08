Get-ChildItem .\ -include bin, obj -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse -ErrorAction SilentlyContinue }
Write-Host "所有 bin 和 obj 文件夹已清理完毕。" -ForegroundColor Green