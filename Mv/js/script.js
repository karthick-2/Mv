/*
  jQuery Document ready
*/
$(function () {
    $('#demo-htmlselect-basic').ddslick(
	{
	    //callback function: do anything with selectedData
	    onSelected: function (data) {
	        displaySelectedData("Callback Function on Dropdown Selection", data);
	    }
	});

    function displaySelectedData(demoIndex, ddData) {
        //alert(ddData.selectedData.value);
        if (ddData.selectedData.value != "") {
            $('#MobileData').load(ddData.selectedData.value);
        }
    }
});