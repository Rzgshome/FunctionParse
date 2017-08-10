# FunctionParse
Compile the string into a function.And have the following characteristics.<br/>
1, analyze the string once while executing it multiple times.<br/>
2, the properties can be dynamically added.<br/>
3, the functions can be dynamically added.<br/>
4, the keywords(properties, functions, and, or, not) are case-insensitive.<br/>
5, the priority of operating characters<br/>
   <table>
   <thead><tr><td>Priority</td><td>operater</td></tr></thead>
   <tbody>
   <tr><td>1</td><td>()</td></tr>
   <tr><td>2</td><td>-※minus</td></tr>
   <tr><td>3</td><td>* /</td></tr>
   <tr><td>4</td><td>+ -※subtract</td></tr>
   <tr><td>5</td><td>=※equal != &lt;&gt; &gt; &gt;= &lt; &lt;= </td></tr>
   <tr><td>6</td><td>AND OR</td></tr>
   <tr><td>7</td><td>NOT</td></tr>
   </tbody>
   </table>
      
