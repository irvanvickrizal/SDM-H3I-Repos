<script language=javascript>
//this script only works with ie

/*@cc_on @*/
/*@if (@_win32 && @_jscript_version>=5)

function window.confirm(str) {
  execScript('n = msgbox("'+str+'","4132","Delete")', "vbscript");
  return(n == 6);
}

@end @*/

if (confirm("{str}")) {
  window.location = document.location.href + "&del=1";
}

</script>
