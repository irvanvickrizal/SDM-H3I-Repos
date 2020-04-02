<%
Data=request.servervariables("QUERY_STRING")
URL = replace(Data,"403","")
URL = replace(URL,"http://","https://")
response.status = "200 OK"
response.redirect URL
%>