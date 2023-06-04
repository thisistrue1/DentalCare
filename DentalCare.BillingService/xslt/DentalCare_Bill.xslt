<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
xmlns:fo="http://www.w3.org/1999/XSL/Format"
xmlns:ibex="http://www.xmlpdf.com/2003/ibex/Format"
xmlns:cs="urn:cs">
  <msxsl:script language="C#" implements-prefix="cs">
    <![CDATA[
        public string dateTimeNow(string format)
        {       
          return DateTime.Now.ToString(format); 
        } 
      ]]>
  </msxsl:script>
<xsl:strip-space elements="*"/>
<xsl:variable name="CurrentDate" select="cs:dateTimeNow('MM-dd-yyyy')"/>
	<xsl:template match="Bill">
		<root xmlns="http://www.w3.org/1999/XSL/Format">
			<layout-master-set>
				<simple-page-master master-name="page-layout">
					<region-body margin="2.5cm" region-name="body"/>
				</simple-page-master>
			</layout-master-set>
			<page-sequence master-reference="page-layout">
			<flow flow-name="body">
					<table-and-caption>
						<table>
							<table-body>
							  <table-row>
								<table-cell>
								  <block>Bill Date: </block>
								</table-cell>
								<table-cell>
								  <block><xsl:value-of select="$CurrentDate"/></block>
								</table-cell>
							  </table-row>							
							  <table-row>
								<table-cell>
								  <block>Patient Name:</block>
								</table-cell>
								<table-cell>
								  <block><xsl:value-of select="PatientName"/></block>
								</table-cell>
							  </table-row>
							  <table-row>
								<table-cell>
								  <block>Billing Name:</block>
								</table-cell>
								<table-cell>
								  <block><xsl:value-of select="BillingName"/></block>
								</table-cell>
							  </table-row>
							  <table-row>
								<table-cell>
								  <block>Billing Address: </block>
								</table-cell>
								<table-cell>
								  <block><xsl:value-of select="BillingAddress"/></block>
								</table-cell>
							  </table-row>
							  <table-row>
								<table-cell>
								  <block>Billing Amount: </block>
								</table-cell>
								<table-cell>
								  <block><xsl:value-of select="BillingAmount"/></block>
								</table-cell>
							  </table-row>
							  <table-row>
								<table-cell>
								  <block>Days Past Due: </block>
								</table-cell>
								<table-cell>
								  <block><xsl:value-of select="DaysPastDue"/></block>
								</table-cell>
							  </table-row>							  
							</table-body>

						</table>
					</table-and-caption>
				</flow>
			</page-sequence>
		</root>
	</xsl:template>
</xsl:stylesheet>
