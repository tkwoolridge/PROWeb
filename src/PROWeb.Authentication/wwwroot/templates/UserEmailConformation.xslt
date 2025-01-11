<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
	version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	exclude-result-prefixes="msxsl">

	<xsl:output method="html" indent="yes"/>

	<xsl:template match="/">
		<html>
			<body>
				<p>
					<div>
						<h4>
							Dear PRO user,
						</h4>
					</div>
					<p>
						Please <a><xsl:attribute name="href"><xsl:value-of select="//ConfirmEmailLink"/></xsl:attribute>confirm</a> your email.
					</p>
					<p>
						<div>Best Regards,</div>
						<div>
							<xsl:value-of select="//GeneralName"/>
						</div>
					</p>
					<p>
						<div>
							<img src="cid:logoId"/>
						</div>
					</p>
				</p>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>